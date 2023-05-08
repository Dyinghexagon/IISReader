import { HttpClient } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { AppConfig } from "../app.config";
import { BaseService } from "./base.service";

@Injectable({
    providedIn: "root"
})

export class NotificatedService extends BaseService {

    public notifications!: Notification[];

    private hubConnection!: signalR.HubConnection;

    constructor(
        http: HttpClient,
        zone: NgZone,
        protected config: AppConfig
    ) {
        super(http, zone);
    }

    public startConnection(): void {
        this.hubConnection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7252/notification", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        }).build();

        this.hubConnection.start()
            .then(() => console.warn("Connection started!"))
            .catch(err => console.log('Error while starting connection: ' + err));
    }

    public addTransferStockDataListener(): void {
        console.warn(this.hubConnection);

        this.hubConnection.on("transferstockdata", (data) => {
            this.notifications = data;
            console.warn(data);
        });
    }

}