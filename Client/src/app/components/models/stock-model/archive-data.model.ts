import { IStockBase } from "./stock-base.model";

export interface IArchiveStockModel extends IStockBase {
    Data: Map<string, IArchiveStockModel>;
}

export interface IArchiveDataModel {
    Open: number;
    Close: number;
    Hight: number;
    Low: number;
    Volume: number;
}