import { Injectable } from "@angular/core";
import { AddNewStockListState } from "./add-new-stock-list-state.module";

@Injectable()
export class ModalState {
    public readonly addNewStockList: AddNewStockListState;

    constructor() {
        this.addNewStockList = new AddNewStockListState();
    }
}