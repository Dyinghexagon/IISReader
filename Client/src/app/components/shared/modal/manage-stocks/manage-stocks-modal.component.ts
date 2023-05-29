import { Component, Input, NgModule, OnInit } from "@angular/core";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import { AccountModel } from "src/app/components/models/account.model";
import { AppState } from "src/app/components/models/app-state.module";
import { IStockListModel } from "src/app/components/models/stock-list.model";
import { IActualStockModel } from "src/app/components/models/stock-model/actual-stock.model";
import { AccountsService } from "src/app/services/accounts.service";

@Component({
    selector: "manage-stocks-modal",
    templateUrl: "./manage-stocks-modal.component.html",
    styleUrls: ["./manage-stocks-modal.component.scss"]
})

export class ManageStocksModalComponent implements OnInit {
    @Input() public mode: "add" | "remove" = "add";
    @Input() public stock!: IActualStockModel;

    public account!: AccountModel | null;

    constructor(
        public modalRef: MdbModalRef<ManageStocksModalComponent>,
        private readonly appState: AppState,
        private readonly accountService: AccountsService
    ) { }

    public async ngOnInit(): Promise<void> {
        this.account = await this.appState.getAccount();
    }

    public get StockList(): IStockListModel[] | undefined {
        switch(this.mode) {
            case "remove": {
                let result: IStockListModel[] = [];
                this.account?.StockList.forEach(stockList => {
                    const index = this.getIndexStockByStockList(stockList);
                    if (index > -1) {
                        result.push(stockList);
                    }
                });
                return result;
            }
        }

        return this.account?.StockList;
    }

    public async selectStockList(index: number): Promise<void> {
        const selectedStockList = this.account?.StockList[index];
        if (selectedStockList) {
            switch(this.mode) {
                case "remove": {
                    this.account?.StockList.forEach(stockList => {
                        const index = this.getIndexStockByStockList(stockList);
                        if (index > -1) {
                            stockList.Stocks.splice(index, 1);
                        }
                    })
                    break;
                }
                default: {
                    const index = this.getIndexStockByStockList(selectedStockList);
                    if (index === -1) {
                        selectedStockList.Stocks.push(this.stock);
                    }
                }
            }
            await this.accountService.updateStockList(this.account?.Id ?? "", selectedStockList);
        }
    }

    private getIndexStockByStockList(stockList: IStockListModel): number {
        let index = 0;
        for(let stock of stockList.Stocks) {
            if (stock.Id === this.stock.Id) {
                return index;
            }
            index++;
        }
        return -1;
    }

    public close(): void {
        this.modalRef.close();
    }
}