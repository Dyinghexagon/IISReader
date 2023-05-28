import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Subject } from "rxjs";
import { StockService } from "src/app/services/stock.service";
import { IActualStockModel } from "../../models/stock-model/actual-stock.model";

@Component({
    selector: "stocks",
    templateUrl: "./stocks.component.html",
    styleUrls: [ "./stocks.component.scss" ]
})

export class StocksComponent implements OnInit, OnDestroy  {

    @Input() public stocks: IActualStockModel[] = [];

    public date: string = "";
    public dtOptions: DataTables.Settings = {};
    public dtTrigger: Subject<any> = new Subject<IActualStockModel[]>();

    constructor(public securityService: StockService) { }

    public async ngOnInit(): Promise<void> {
        this.dtOptions = {
            pagingType: 'full_numbers',
            pageLength: 10,
            processing: true
        };

        if (!this.stocks.length) {
            this.stocks = await this.securityService.getSecurityList();
        }

        this.dtTrigger.next(this.stocks);
    }

    public ngOnDestroy(): void {
        this.dtTrigger.unsubscribe();
    }

}
