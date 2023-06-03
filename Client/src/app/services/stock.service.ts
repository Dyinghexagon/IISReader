import { HttpClient } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import { AppConfig } from "../app.config";
import { IActualStockModel } from "../components/models/stock-model/actual-stock.model";
import { IStockChartDataModel } from "../components/models/stock-model/stock-char-data.mnodel";
import { BaseService } from "./base.service";

@Injectable()
export class StockService extends BaseService {

    constructor(
        http: HttpClient,
        zone: NgZone,
        protected config: AppConfig
    ) {
        super(http, zone);
    }

    public getSecurityList(): Promise<IActualStockModel[]> {
        return this.get(`${this.config.stocksApi}/GetStocksList`).then(data => data.body);
    }

    public async getSecurityChartData(secid: string): Promise<IStockChartDataModel[]> {
        return this.get(`${this.config.stocksApi}/GetStockChartData/${secid}`).then(data => data.body);
    }

    public async initArchiveStocks(): Promise<void> {
        return this.get(`${this.config.stocksApi}/InitArchiveStock`).then(data => data.body);
    }

}
