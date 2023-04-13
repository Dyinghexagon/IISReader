import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { StockModel } from "../components/models/stock.model";
import { SecurityChartDataModel } from "../components/models/securityChartDataModel.model";
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
        return this.get(`${this.config.stocksApi}/GetSecuritysList`);
    }

    public async getSecurityChartData(secid: string): Promise<SecurityChartDataModel[]> {
        return this.get(`${this.config.stocksApi}/GetSecurityChartData/${secid}`);
    }

    public async fillingStocks(): Promise<void> {
        return this.get(`${this.config.stocksApi}/FillingStocks`);
    }
}