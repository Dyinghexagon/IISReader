import { Subject } from "rxjs";
import { IStockListModel } from "./stock-list.model";

export class StockListState {

    public readonly createdStockList$ = new Subject<ICreateStockListCOnfig>();
    public readonly editedStockList$ = new Subject<IStockListModel>();

}

export interface ICreateStockListCOnfig {
    stockList: IStockListModel;
    isAddAllStocks: boolean;
}