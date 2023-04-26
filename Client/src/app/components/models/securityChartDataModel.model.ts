export class StockChartDataModel {
    private _id: string;
    private _open: number;
    private _close: number;
    private _hight: number;
    private _low: number;
    private _time: string;

    constructor(data: IStockChartDataModel) {
        this._id = data.id;
        this._open = data.open;
        this._close = data.close;
        this._hight = data.hight;
        this._low = data.low;
        this._time = data.time;
    }

    public get Id(): string {
        return this._id;
    }

    public get Open(): number {
        return this._open;
    }

    public get Close(): number {
        return this._close;
    }

    public get Hight(): number {
        return this._hight;
    }

    public get Low(): number {
        return this._low;
    }

    public get Time(): string {
        return this._time;
    }
}

export interface IStockChartDataModel {
    id: string;
    open: number;
    close: number;
    hight: number;
    low: number;
    time: string;
}