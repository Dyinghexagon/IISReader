import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { StockModel } from "../components/models/stock.model";
import { StockChartDataModel } from "../components/models/securityChartDataModel.model";
import { BaseService } from "./base.service";

@Injectable()
export class StockService extends BaseService {

    constructor(
        http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public getSecurityList(): Promise<StockModel[]> {
        return this.get(`${this.config.stocksApi}/GetStocksList`);
    }

    public async getSecurityChartData(secid: string): Promise<StockChartDataModel[]> {
        return this.get(`${this.config.stocksApi}/GetStockChartData/${secid}`);
    }

}