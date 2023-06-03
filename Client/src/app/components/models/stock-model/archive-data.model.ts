export interface IArchiveDataModel {
    Data: Map<string, IArchiveDataModel>;
}

export interface IArchiveStockModel {
    Open: number;
    Close: number;
    Hight: number;
    Low: number;
    Volumn: number;
}