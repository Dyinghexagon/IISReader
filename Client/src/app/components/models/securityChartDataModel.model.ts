export class StockChartDataModel {
    private id: string;
    private open: number;
    private close: number;
    private hight: number;
    private low: number;
    private time: string;

    constructor(data: IStockChartDataModel) {
        this.id = data.id;
        this.open = data.open;
        this.close = data.close;
        this.hight = data.hight;
        this.low = data.low;
        this.time = data.time;
    }

    public get Id(): string {
        return this.id;
    }

    public get Open(): number {
        return this.open;
    }

    public get Close(): number {
        return this.close;
    }

    public get Hight(): number {
        return this.hight;
    }

    public get Low(): number {
        return this.low;
    }

    public get Time(): string {
        return this.time;
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