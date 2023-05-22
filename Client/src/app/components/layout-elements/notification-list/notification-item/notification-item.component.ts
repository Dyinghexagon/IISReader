import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { INotification } from "src/app/components/models/notification.model";

@Component({
    selector: "notification-item",
    templateUrl: "./notification-item.component.html",
    styleUrls: ["./notification-item.component.scss"]
})
export class NotificationItem implements OnInit{

    @Input() public notification!: INotification;
    @Output() public notificationChange = new EventEmitter<INotification>();

    public ngOnInit(): void {
        console.warn(this.notification);
    }

    public readNotification() {
        this.notification.IsReaded = true;
        this.notificationChange.emit(this.notification);
    }
}