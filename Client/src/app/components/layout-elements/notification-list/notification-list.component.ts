import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
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

    public get NotificationsStab(): INotification[] {
        return [
            {
                Id: "1",
                Title: "Тестовое уведомление №1",
                Description: "Тестовое описание №1",
                SecId: "GAZP",
                Volume: 1,
                Date: new Date(),
                IsReaded: false
            },
            {
                Id: "2",
                Title: "Тестовое уведомление №2",
                Description: "Тестовое описание №2",
                SecId: "SMLT",
                Volume: 2,
                Date: new Date(),
                IsReaded: false
            },
            {
                Id: "3",
                Title: "Тестовое уведомление №3",
                Description: "Тестовое описание №3",
                SecId: "YNDX",
                Volume: 3,
                Date: new Date(),
                IsReaded: false
            },
            {
                Id: "4",
                Title: "Тестовое уведомление №4",
                Description: "Тестовое описание №4",
                SecId: "POSI",
                Volume: 4,
                Date: new Date(),
                IsReaded: false
            },
        ];
    }

    public changeNotificationStatus(event: INotification): void {
        const targetNotification = this.account?.Notifications.find(notification => notification.Id === event.Id);
        if (targetNotification) {
            targetNotification.IsReaded = event.IsReaded;
            this.accountChange.emit(this.account);
        }
    }
}