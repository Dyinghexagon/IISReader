import { Component, EventEmitter, Input, Output } from "@angular/core";
import { AccountModel } from "../../models/account.model";
import { INotification } from "../../models/notification.model";

@Component({
    selector: "notification-list",
    templateUrl: "./notification-list.component.html",
    styleUrls: ["./notification-list.component.scss"]
})
export class NotificationList {

    @Input() account!: AccountModel | null;
    @Output() accountChange = new EventEmitter<AccountModel | null>();

    public p: number = 1;

    constructor() {}

    public get Notifications(): INotification[] {
        const notificationsIsReaded = this.account?.Notifications.filter(notification => notification.IsReaded);
        const notificationsNotReaded = this.account?.Notifications.filter(notification => !notification.IsReaded);
        return notificationsIsReaded && notificationsNotReaded ? [...notificationsNotReaded, ...notificationsIsReaded] : [];
    }

    public changeNotificationStatus(event: INotification): void {
        const targetNotification = this.account?.Notifications.find(notification => notification.Id === event.Id);
        if (targetNotification) {
            targetNotification.IsReaded = event.IsReaded;
            this.accountChange.emit(this.account);
        }
    }
}