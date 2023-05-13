import { Injectable } from "@angular/core";

@Injectable()
export class AppConfig {
    public get accountsApi(): string { return "/api/accounts"; }
    public get authApi(): string { return "/api/auth"; }
    public get stocksApi(): string { return "/api/stocks"; }
    public get notificationApi(): string { return "https://localhost:7252/notification"; }
}
