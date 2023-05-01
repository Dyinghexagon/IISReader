import { Subject } from "rxjs";
import { StockListModel } from "./stock-list.model";

export class StockListState {

    public readonly createdStockList$ = new Subject<StockListModel>();
    public readonly editedStockList$ = new Subject<StockListModel>();

}