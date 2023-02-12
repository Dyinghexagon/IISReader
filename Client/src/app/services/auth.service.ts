import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { AccountModel } from "../components/models/account.model";
import { BaseService } from "./base.service";

@Injectable()
export class AuthService extends BaseService {

    constructor(
        http: HttpClient,
        protected config: AppConfig,
    ) {
        super(http);
    }

    public register(account: AccountModel): Promise<any> {
        return this.post(`${this.config.authApi}/register`, account, this.jwt());
    }

    public login(account: AccountModel): Promise<number> {
        return this.post(`${this.config.authApi}/authenticate`, account).then((token: Response) => {
            if (token) {
                localStorage.setItem("currentAccount", JSON.stringify({id: account.id, login: account.login}));
            }
            return !token ? 404 : token.status;
        });
    }

    public logout() {
        localStorage.removeItem("currentAccount");
    }

    private jwt() {
        if (!localStorage.length) return new HttpHeaders({ "content-type": "application/json", "cache-control": "no-cache" });
        let token = JSON.parse(localStorage.getItem("currentAccount") ?? "").token;
        return new HttpHeaders({
            "Accept":"application/json",
            "Authorization": "Bearer " + token
        });
    }
}