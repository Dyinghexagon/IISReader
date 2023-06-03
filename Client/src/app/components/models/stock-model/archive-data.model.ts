export interface IArchiveDataModel {
    Data: IActualStockModel[];
}

export interface IActualStockModel {
    Open: number;
    Close: number;
    Hight: number;
    Low: number;
    Time: string;
    Volumn: number;
}