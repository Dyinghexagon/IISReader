export class SecurityModel {
    private _tiket: string;
    private _name: string;
    private _currentPrice: number;
    private _changePerDay: number;

    constructor(data: ISecurityModel) {
        this._tiket = data.tiket;
        this._name = data.name;
        this._currentPrice = data.currentPrice;
        this._changePerDay = data.changePerDay;
    }

    public get Tiket(): string {
        return this._tiket;
    }
    
    public get Name(): string {
        return this._name;
    }
    
    public get CurrentPrice(): number {
        return this._currentPrice;
    }
    
    public get ChangePerDay(): number {
        return this._changePerDay;
    }
}

export interface ISecurityModel {
    tiket: string;
    name: string;
    currentPrice: number;
    changePerDay: number;
}