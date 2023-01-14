import { Injectable } from "@angular/core";

@Injectable()
export class AppConfig {
    public get accountApi(): string { return "/api/account"; }
    public get securitysApi(): string { return "/api/securitys"; }
}