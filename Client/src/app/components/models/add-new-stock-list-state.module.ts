import { Subject } from "rxjs";
import { IStockListModel } from "./stock-list.model";

export class StockListState {

    public readonly createdStockList$ = new Subject<IStockListModel>();
    public readonly editedStockList$ = new Subject<IStockListModel>();

}
