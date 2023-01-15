import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { SecurityModel } from "../components/models/security.model";
import { BaseService } from "./base.service";

@Injectable()
export class SecurityService extends BaseService {

    constructor(
        http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public getSecurityList(): Promise<SecurityModel[]> {
        return this.get(`${this.config.securitysApi}/GetSecuritysList`);
    }
}