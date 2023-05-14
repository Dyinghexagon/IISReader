import { Component, OnDestroy, OnInit } from "@angular/core";
import { data } from "jquery";
import { Subject, takeUntil } from "rxjs";
import { NotificatedService } from "src/app/services/notification.service";
import { AccountModel } from "../../models/account.model";
import { AppState } from "../../models/app-state.module";
import { NotificationModel } from "../../models/notification.model";

@Component({
    selector: "notification-list",
    templateUrl: "./notification-list.component.html",
    styleUrls: ["./notification-list.component.scss"]
})
export class NotificationList implements OnInit, OnDestroy {

    public account: AccountModel | null = null;
    private readonly unsubscribe$ = new Subject<void>();

    constructor(
        private readonly appstate: AppState,
        private readonly notificatedService: NotificatedService
    ) {
    }

    public async ngOnInit(): Promise<void> {
        this.account = await this.appstate.getAccount();

        this.notificatedService.notificateSend$
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(async () => {
                this.account = await this.appstate.getAccount();
            });
    }

    public ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    public get Notifications(): NotificationModel[] | undefined {
        const notificationsIsReaded = this.account?.Notifications.filter(notification => notification.IsReaded);
        const notificationsNotReaded = this.account?.Notifications.filter(notification => !notification.IsReaded);
        return notificationsIsReaded && notificationsNotReaded ? [...notificationsNotReaded, ...notificationsIsReaded] : undefined;

    }
}