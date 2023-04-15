import { StockListModel } from "./stock-list.model";

export class AccountModel {
    
    public id: string;
    public login: string;
    public password: string;
    public stockList?: StockListModel | null;

    constructor(data: IAccountModel) {
        this.id = data.id;
        this.login = data.login;
        this.password = data.password;
        this.stockList = data.stockList;
    }

}

export interface IAccountModel {
    id: string;
    login: string;
    password: string;
    stockList?: StockListModel | null;
}