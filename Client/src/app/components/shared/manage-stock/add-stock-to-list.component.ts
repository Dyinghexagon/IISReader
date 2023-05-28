import { Component, Input } from "@angular/core";
import { IActualStockModel } from "../../models/stock-model/actual-stock.model";

@Component({
    selector: "manage-stock",
    templateUrl: "./add-stock-to-list.component.html",
    styleUrls: ["./add-stock-to-list.component.scss"]
})

export class ManageStockComponent {
    @Input() public stock!: IActualStockModel;

    public addStockToList(): void {
        console.warn("addStockToList", this.stock);
    }

    public removeStockFromList(): void {
        console.warn("removeStockFromList", this.stock);
    }
}