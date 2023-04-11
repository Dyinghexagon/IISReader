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

    public register(account: AccountModel): Promise<number> {
        return this.post(`${this.config.authApi}/register`, account, this.jwt()).then((response: Response) => {
            return response.status
        });
    }

    public login(account: AccountModel): Promise<number> {
        return this.post(`${this.config.authApi}/authenticate`, account).then((response: Response) => {
            if (response) {
                localStorage.setItem("currentAccount", JSON.stringify({id: account.id, login: account.login}));
            }

            return response.status;
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