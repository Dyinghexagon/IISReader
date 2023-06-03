import { IActualStockModel } from "./stock-model/actual-stock.model";
import { CalculationType } from "./stock-model/stock-base.model";

export interface IStockListModel {
  Id: string;
  Title: string;
  Stocks: IActualStockModel[];
  IsNotificated: boolean;
  CalculationType: CalculationType;
  Ratio: number;
}
