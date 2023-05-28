import { IStockBase } from "./stock-base.model";

export interface IActualStockModel extends IStockBase {
  secId: string;
  name: string;
  currentPrice: number;
  changePerDay: number;
  currentVolume: number;
}
