import { Component, Input } from "@angular/core";
import { MdbModalRef, MdbModalService } from "mdb-angular-ui-kit/modal";
import { IActualStockModel } from "../../models/stock-model/actual-stock.model";
import { ManageStocksModalComponent } from "../modal/manage-stocks/manage-stocks-modal.component";

@Component({
    selector: "manage-stock",
    templateUrl: "./manage-stock.component.html",
    styleUrls: ["./manage-stock.component.scss"]
})

export class ManageStockComponent {
    @Input() public stock!: IActualStockModel;

    public manageStocksModalRef: MdbModalRef<ManageStocksModalComponent> | null = null;
    
    constructor(private readonly modalService: MdbModalService) {}

    public addStockToList(): void {
        this.manageStocksModalRef = this.modalService.open(ManageStocksModalComponent, {
            data: { 
                mode: "add",
                stock: this.stock
            }
        });
    }

    public removeStockFromList(): void {
        this.manageStocksModalRef = this.modalService.open(ManageStocksModalComponent, {
            data: { 
                mode: "remove",
                stock: this.stock
            }
        });
    }
}