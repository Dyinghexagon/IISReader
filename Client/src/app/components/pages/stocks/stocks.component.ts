import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Subject } from "rxjs";
import { StockService } from "src/app/services/stock.service";
import { IActualStockModel } from "../../models/stock-model/actual-stock.model";

@Component({
    selector: "stocks",
    templateUrl: "./stocks.component.html",
})

export class StocksComponent implements OnInit, OnDestroy  {

    @Input() public stocks: IActualStockModel[] = [];
    @Input() public isNeedAllStock?: boolean = true;

    public dtOptions: DataTables.Settings = {};
    public dtTrigger: Subject<any> = new Subject<IActualStockModel[]>();

    constructor(
        public securityService: StockService,
        private route: ActivatedRoute
    ) { }

    public async ngOnInit(): Promise<void> {   
        this.dtOptions = {
            pagingType: 'full',
            pageLength: 10,
            paging: true,
            processing: true,
            autoWidth: true
        };
    
        this.route.queryParams.subscribe(params => {
            this.isNeedAllStock = params["isNeedAllStock"];
        });

        if (this.isNeedAllStock) {
            this.stocks = await this.securityService.getSecurityList();
        }

        this.dtTrigger.next(this.stocks);
    }

    public ngOnDestroy(): void {
        this.dtTrigger.unsubscribe();
    }

}
