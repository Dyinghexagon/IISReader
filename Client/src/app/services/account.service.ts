import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { AccountModel } from "../components/models/account.model";
import { BaseService } from "./base.service";

@Injectable()
export class AccountService extends BaseService {
    public user?: AccountModel;

    constructor(
        http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public getAccount(login: string): Promise<AccountModel> {
        return this.get(`${this.config.accountApi}/GetAccount/${login}`);
    }

    public getAcccounts(): Promise<AccountModel[]> {
        return this.get(`${this.config.accountApi}/GetAccounts`);
    }

    public createUser(user: AccountModel): Promise<any> {
        return this.post(`${this.config.accountApi}/register`, user, this.jwt());
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