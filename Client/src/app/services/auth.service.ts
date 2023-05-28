import { HttpClient } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import { AppConfig } from "../app.config";
import { AccountModel } from "../components/models/account.model";
import { BaseService } from "./base.service";

@Injectable()
export class AuthService extends BaseService {

    constructor(
        http: HttpClient,
        zone: NgZone,
        protected config: AppConfig
    ) {
        super(http, zone);
    }

    public get loginKey(): string {
        return "currentAccount";
    }

    public register(account: AccountModel): Promise<void> {
        return this.post(`${this.config.authApi}/register`, account).then(data => data);
    }

    public login(account: AccountModel): Promise<number> {
        return this.post(`${this.config.authApi}/authenticate`, account).then(response => {
            if (response.status === 200) {
                const data = response.body.Value;
                localStorage.setItem(this.tokenKey, data.acces_token);
                localStorage.setItem(this.loginKey, data.login);
            }
            return response.status;
        });
    }

    public logout() {
        localStorage.removeItem(this.tokenKey);
        localStorage.removeItem(this.loginKey);
    }
}
