import { Component, OnInit } from "@angular/core";
import { AlertService } from "src/app/services/alert.service";
import { IAlert } from "../../models/alert.model";

@Component({
    moduleId: module.id,
    selector: "",
    templateUrl: "./alert.component.html"
})

export class AllertComponent implements OnInit {
    public alert!: IAlert;

    constructor(private alertService: AlertService) {
    }

    public ngOnInit(): void {
        this.alertService.message.subscribe(m => {
            this.alert = m;
        });
    }
}