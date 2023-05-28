import { HttpClient } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import { AppConfig } from "../app.config";
import { AccountModel } from "../components/models/account.model";
import { IStockListModel } from "../components/models/stock-list.model";
import { BaseService } from "./base.service";

@Injectable()
export class AccountsService extends BaseService {

    constructor(
        http: HttpClient,
        zone: NgZone,
        protected config: AppConfig
    ) {
        super(http, zone);
    }

    public getAccount(login: string): Promise<AccountModel> {
        return this.get(`${this.config.accountsApi}/GetAccount/${login}`).then(data => data.body);
    }

    public getAcccounts(): Promise<AccountModel[]> {
        return this.get(`${this.config.accountsApi}/GetAccounts`);
    }

    public setNewStockList(accountId: string, stockList: IStockListModel): Promise<void> {
      return this.post(`${this.config.accountsApi}/SetNewStockList/${accountId}`, stockList);
    }

    public updateAccount(accountId: string, account: AccountModel | null): Promise<void> {
        return this.post(`${this.config.accountsApi}/update/${accountId}`, account);
    }

}
