import { Component, OnInit } from "@angular/core";
import { AlertService } from "src/app/services/alert.service";
import { IAlert } from "../../models/alert.model";

@Component({
    selector: "alert",
    templateUrl: "./alert.component.html",
    styleUrls: ["./alert.component.scss"]
})

export class AlertComponent implements OnInit {
    public alert!: IAlert;
    public close: boolean = true;

    constructor(private alertService: AlertService) {
    }

    public ngOnInit(): void {
        this.alertService.message.subscribe(alert => {
            this.alert = alert;
            this.close = true;
        });
    }

}