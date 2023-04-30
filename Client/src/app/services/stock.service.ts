import { HttpClient } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import { AppConfig } from "../app.config";
import { StockModel } from "../components/models/stock.model";
import { StockChartDataModel } from "../components/models/securityChartDataModel.model";
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

    public getSecurityList(): Promise<StockModel[]> {
        return this.get(`${this.config.stocksApi}/GetStocksList`).then(data => data.body);
    }

    public async getSecurityChartData(secid: string): Promise<StockChartDataModel[]> {
        return this.get(`${this.config.stocksApi}/GetStockChartData/${secid}`).then(data => data.body);
    }

}