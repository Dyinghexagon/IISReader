import { NotificationModel } from "./notification.model";
import { StockListModel } from "./stock-list.model";

export class AccountModel {
    
    private id: string;
    private login: string;
    private password: string;
    private stockLists: StockListModel[] = [];
    private notifications: NotificationModel[] = [];

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

    public get StockList(): StockListModel[] {
        return this.stockLists;
    }

    public get Notifications(): NotificationModel[] {
        return this.notifications;
    }

}

export interface IAccountModel {
    id: string;
    login: string;
    password: string;
    stockLists: StockListModel[];
    notifications: NotificationModel[];
}