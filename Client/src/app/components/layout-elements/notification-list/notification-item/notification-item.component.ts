import { Component, EventEmitter, Input, Output } from "@angular/core";
import { INotification } from "src/app/components/models/notification.model";

@Component({
    selector: "notification-item",
    templateUrl: "./notification-item.component.html",
    styleUrls: ["./notification-item.component.scss"]
})
export class NotificationItem {

    @Input() public notification!: INotification;
    @Output() public notificationChange = new EventEmitter<INotification>();

    public readNotification() {
        this.notification.IsReaded = true;
        this.notificationChange.emit(this.notification);
    }
}