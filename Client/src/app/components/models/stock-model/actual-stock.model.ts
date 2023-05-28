import { IStockBase } from "./stock-base.model";

export interface IActualStockModel extends IStockBase {
  Name: string;
  CurrentPrice: number;
  ChangePerDay: number;
  CurrentVolume: number;
}
