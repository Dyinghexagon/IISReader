import { Subject } from "rxjs";
import { StockListModel } from "./stock-list.model";

export class AddNewStockListState{
    public readonly createdList$ = new Subject<StockListModel>();
}