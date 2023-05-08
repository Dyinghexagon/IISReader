export class StockModel {
    private id: string;
    private secid: string;
    private name: string;
    private currentPrice: number;
    private changePerDay: number;
    private currentVolume: number;

    constructor(data: IStockModel) {
        this.id = data.id;
        this.secid = data.secid;
        this.name = data.name;
        this.currentPrice = data.currentPrice;
        this.changePerDay = data.changePerDay;
        this.currentVolume = data.currentVolume;
    }

    public get Id(): string {
        return this.id;
    }

    public get SecId(): string {
        return this.secid;
    }
    
    public get Name(): string {
        return this.name;
    }
    
    public get CurrentPrice(): number {
        return this.currentPrice;
    }
    
    public get ChangePerDay(): number {
        return this.changePerDay;
    }

    public get CurrentVolume(): number {
        return this.currentVolume;
    }
}

export interface IStockModel {
    id: string;
    secid: string;
    name: string;
    currentPrice: number;
    changePerDay: number;
    currentVolume: number;
}