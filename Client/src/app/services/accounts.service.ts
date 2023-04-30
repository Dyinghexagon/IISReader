import { HttpClient } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import { AppConfig } from "../app.config";
import { AccountModel } from "../components/models/account.model";
import { StockListModel } from "../components/models/stock-list.model";
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
        return this.get(`${this.config.accountsApi}/GetAccount/${login}`);
    }

    public getAcccounts(): Promise<AccountModel[]> {
        return this.get(`${this.config.accountsApi}/GetAccounts`);
    }

    public setNewStockList(accountId: string, stockList: StockListModel): Promise<void> {
        return this.post(`${this.config.accountsApi}/setNewStockList`, {
            id: accountId, stockList: stockList
        });
    }

}