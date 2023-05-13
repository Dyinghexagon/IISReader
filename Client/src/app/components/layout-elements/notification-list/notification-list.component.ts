import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subject, takeUntil } from "rxjs";
import { NotificatedService } from "src/app/services/notification.service";
import { AccountModel } from "../../models/account.model";
import { AppState } from "../../models/app-state.module";

@Component({
    selector: "notification-list",
    templateUrl: "./notification-list.component.html",
    styleUrls: ["./notification-list.component.scss"]
})
export class NotificationList implements OnInit, OnDestroy {

    public account!: AccountModel | null;
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
                setTimeout(async () => {
                    this.account = await this.appstate.getAccount();
                }, 100);
        });
    }

    public ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}