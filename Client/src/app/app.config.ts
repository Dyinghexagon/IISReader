import { Injectable } from "@angular/core";

@Injectable()
export class AppConfig {
    public get accountApi(): string { return "/accountApi"; }
    public get securitysApi(): string { return "/securitysApi"; }
}