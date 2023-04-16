import { StockListModel } from "./stock-list.model";

export class AccountModel {
    
    public id: string;
    public login: string;
    public password: string;
    public stockLists: StockListModel[] = [];

    constructor(data: IAccountModel) {
        this.id = data.id;
        this.login = data.login;
        this.password = data.password;
        this.stockLists = [];
    }

    public newStockList(stockList: StockListModel): void {
        this.stockLists?.push(stockList);
    }
}

export interface IAccountModel {
    id: string;
    login: string;
    password: string;
    stockLists: StockListModel[];
}