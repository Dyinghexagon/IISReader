import { Component, Input } from "@angular/core";
import { data } from "jquery";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import { IAlert } from "../../models/alert.model";

@Component({
    selector: "alert",
    templateUrl: "./alert.component.html",
    styleUrls: ["./alert.component.scss"]
})

export class AlertModalComponent {
    @Input() public alert!: IAlert; 

    constructor(public modalRef: MdbModalRef<AlertModalComponent>) {}

    public get Date(): string {
        const date = this.alert.date;
        return `${date.getDay()}.${date.getMonth()}.${date.getFullYear()}`;
    }

    public close(): void {
        this.modalRef.close();
    }

}