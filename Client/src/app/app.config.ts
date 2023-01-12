import { Injectable } from "@angular/core";

@Injectable()
export class AppConfig {
    public get userApi(): string { return "/user"; }
}