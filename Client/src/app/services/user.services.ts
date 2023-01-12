import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AppConfig } from "../app.config";

@Injectable()
export class UserService {
    protected apiRoot: string;

    constructor(
        protected http: HttpClient,
        protected config: AppConfig,
    ) {
        this.apiRoot = this.config.userApi;
    }

    public getUser(): Observable<any> {
        return this.http.get(`${this.apiRoot}`);
    }

}