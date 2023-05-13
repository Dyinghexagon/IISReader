import { NotificationModel } from "./notification.model";
import { StockListModel } from "./stock-list.model";

export class AccountModel {
    
    private _id: string;
    private _login: string;
    private _password: string;
    private _stockLists: StockListModel[] = [];
    private _notifications: NotificationModel[] = [];

    constructor(data: IAccountModel) {
        this._id = data.id;
        this._login = data.login;
        this._password = data.password;
        this._stockLists = data.stockLists;
        this._notifications = data.notifications;
    }

    public get Id(): string {
        return this._id;
    }

    public get Login(): string {
        return this._login;
    }

    public get Password(): string {
        return this._password;
    }

    public get StockList(): StockListModel[] {
        return this._stockLists;
    }

    public get Notifications(): NotificationModel[] {
        return this._notifications;
    }

}

export interface IAccountModel {
    id: string;
    login: string;
    password: string;
    stockLists: StockListModel[];
    notifications: NotificationModel[];
}