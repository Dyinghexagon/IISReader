import { Injectable } from "@angular/core";
import { StockListState } from "./add-new-stock-list-state.module";

@Injectable()
export class ModalState {
    public readonly stockListState: StockListState;

    constructor() {
        this.stockListState = new StockListState();
    }
}