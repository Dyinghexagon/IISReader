import { HttpClient } from "@angular/common/http";
import { Injectable, NgZone, OnDestroy } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { Subject } from "rxjs";
import { AppConfig } from "../app.config";
import { AlertService } from "./alert.service";
import { BaseService } from "./base.service";

@Injectable({
    providedIn: "root"
})

export class NotificatedService extends BaseService implements OnDestroy {
    private hubConnection: signalR.HubConnection;

    public notificateSend$: Subject<void> = new Subject<void>();

    constructor(
        http: HttpClient,
        zone: NgZone,
        protected config: AppConfig,
        private readonly alertService: AlertService
    ) {
        super(http, zone);
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(config.notificationApi, {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            }
        ).build();
        this.hubConnection.start()
            .then(() => {
            })
            .catch(err => console.log('Error while starting connection: ' + err));

        this.hubConnection.on("transferstockdata", () => {
            this.notificateSend$.next();
            this.alertService.createAllert(200, "Уведомление о нахождении аномальной сделки", "Аномальная сделка обнаружена", "Ошибка!");
        });
    }

    public ngOnDestroy(): void {
        this.notificateSend$.unsubscribe();
    }



}