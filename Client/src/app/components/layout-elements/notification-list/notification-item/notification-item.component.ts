import { Component, Input } from "@angular/core";
import { NotificationModel } from "src/app/components/models/notification.model";

@Component({
    selector: "notification-item",
    templateUrl: "./notification-item.component.html",
    styleUrls: ["./notification-item.component.scss"]
})
export class NotificationItem {
    @Input() public notification!: NotificationModel;

    public readNotification() {
        this.notification.IsReaded = true;
    }
}