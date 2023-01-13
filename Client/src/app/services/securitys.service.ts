import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AppConfig } from "../app.config";
import { SecurityModel } from "../components/models/security.model";
import { BaseService } from "./base.service";

@Injectable()
export class SecurityService extends BaseService {

    constructor(
        protected http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public getSecurityList(): Observable<SecurityModel[]> {
        return this.http.get<SecurityModel[]>(`${this.config.securitysApi}`);
    }
}