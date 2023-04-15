import { StockModel } from "./stock.model";

export class StockListModel {
    public id: string;
    public title: string;
    public stocks: StockModel[];

    constructor(data: IStockListModel) {
        this.id = data.id;
        this.title = data.title;
        this.stocks = data.stocks;
    }
}

export interface IStockListModel {
    id: string;
    title: string;
    stocks: StockModel[];
}