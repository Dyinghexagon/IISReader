import { StockModel } from "./stock.model";

export class StockListModel {
    
    private id: string;
    private title: string;
    private stocks: StockModel[];
    private isNotificated: boolean;

    constructor(data: IStockListModel) {
        this.id = data.id;
        this.title = data.title;
        this.stocks = data.stocks;
        this.isNotificated = data.isNotificated;
    }

    public get Id() {
        return this.id;
    }

    public get Title() {
        return this.title;
    }

    public get Stocks() {
        return this.stocks;
    }

    public get IsNotificated() {
        return this.isNotificated;
    }

    public set IsNotificated(value: boolean) {
        this.isNotificated = value ?? true;
    }
}

export interface IStockListModel {
    id: string;
    title: string;
    stocks: StockModel[];
    isNotificated: boolean;
}