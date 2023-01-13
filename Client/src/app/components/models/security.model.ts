export class SecurityModel {
    public tiket: string;
    public name: string;
    public currentPrice: number;
    public changePerDay: number;

    constructor(data: ISecurityModel) {
        this.tiket = data.tiket;
        this.name = data.name;
        this.currentPrice = data.currentPrice;
        this.changePerDay = data.changePerDay;
    }
}

export interface ISecurityModel {
    tiket: string;
    name: string;
    currentPrice: number;
    changePerDay: number;
}