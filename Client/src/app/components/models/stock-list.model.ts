import { IActualStockModel } from "./stock-model/actual-stock.model";
import { CalculationType } from "./stock-model/stock-base.model";

export interface IStockListModel {

  id: string;
  title: string;
  stocks: IActualStockModel[];
  isNotificated: boolean;
  calculationType: CalculationType
}
