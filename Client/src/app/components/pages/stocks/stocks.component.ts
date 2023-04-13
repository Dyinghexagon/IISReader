import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subject } from "rxjs";
import { StockService } from "src/app/services/stock.service";
import { StockModel } from "../../models/stock.model";

@Component({
    selector: "stocks",
    templateUrl: "./stocks.component.html",
    styleUrls: [ "./stocks.component.scss" ]
})

export class StocksComponent implements OnInit, OnDestroy  {

    public stocks: StockModel[] = [];
    public date: string = "";
    public dtOptions: DataTables.Settings = {};
    public dtTrigger: Subject<any> = new Subject<StockModel[]>();
    
    constructor(public securityService: StockService) { }

    public async ngOnInit(): Promise<void> {
        this.dtOptions = {
            pagingType: 'full_numbers',
            pageLength: 10,
            processing: true
        };
        this.stocks = await this.securityService.getSecurityList();
        this.dtTrigger.next(this.stocks);
    }

    public ngOnDestroy(): void {
        this.dtTrigger.unsubscribe();
    }

}
