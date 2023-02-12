import { HttpClient } from "@angular/common/http";
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

}