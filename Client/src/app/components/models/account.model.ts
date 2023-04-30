import { StockListModel } from "./stock-list.model";

export class AccountModel {
    
    private id: string;
    private login: string;
    private password: string;
    private stockLists: StockListModel[] = [];

    constructor(data: IAccountModel) {
        this.id = data.id;
        this.login = data.login;
        this.password = data.password;
        this.stockLists = [];
    }

    public get Id() {
        return this.id;
    }

    public get Login() {
        return this.login;
    }

    public get Password() {
        return this.password;
    }

    public get StockList() {
        return this.stockLists;
    }

}

export interface IAccountModel {
    id: string;
    login: string;
    password: string;
    stockLists: StockListModel[];
}