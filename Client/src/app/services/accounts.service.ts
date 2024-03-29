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

    public setNewStockList(accountId: string, stockList: IStockListModel, isAddAllStocks: boolean): Promise<AccountModel> {
        return this.post(`${this.config.accountsApi}/SetNewStockList/${accountId}/${isAddAllStocks}`, stockList).then(data => data.body);
    }

    public updateStockList(accountId: string, stockList: IStockListModel): Promise<AccountModel> {
        return this.post(`${this.config.accountsApi}/UpdateStockList/${accountId}`, stockList).then(data => data.body);
    }

    public updateAccount(accountId: string, account: AccountModel | null): Promise<AccountModel> {
        return this.post(`${this.config.accountsApi}/Update/${accountId}`, account).then(data => data.body);
    }

}
