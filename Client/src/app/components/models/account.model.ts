import { INotification } from "./notification.model";
import { IStockListModel } from "./stock-list.model";

export class AccountModel {

    private id: string;
    private login: string;
    private password: string;
    private stockLists: IStockListModel[] = [];
    private notifications: INotification[] = [];

    constructor(data: IAccountModel) {
        this.id = data.id;
        this.login = data.login;
        this.password = data.password;
        this.stockLists = data.stockLists;
        this.notifications = data.notifications;
    }

    public get Id(): string {
        return this.id;
    }

    public get Login(): string {
        return this.login;
    }

    public get Password(): string {
        return this.password;
    }

    public get StockList(): IStockListModel[] {
        return this.stockLists;
    }

    public get Notifications(): INotification[] {
        return this.notifications;
    }

}

export interface IAccountModel {
    id: string;
    login: string;
    password: string;
    stockLists: IStockListModel[];
    notifications: INotification[];
}
