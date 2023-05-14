export class NotificationModel {
    private title: string;
    private description: string;
    private secId: string;
    private volume: number;
    private date: Date;
    private isReaded: boolean;

    constructor(data: INotificationModel) {
        this.title = data.title;
        this.description = data.description;
        this.secId = data.secId;
        this.volume = data.volume;
        this.date = data.date;
        this.isReaded = data.isReaded;
    }

    public get Title(): string {
        return this.title;
    }

    public get Description(): string {
        return this.description;
    }

    public get SecId(): string {
        return this.secId;
    }

    public get Volume(): number {
        return this.volume;
    }

    public get Date(): Date {
        return this.date;
    }

    public get IsReaded(): boolean {
        return this.isReaded;
    }

    public set IsReaded(value: boolean) {
        this.isReaded = value;
    }

}

export interface INotificationModel {
    title: string;
    description: string;
    secId: string;
    volume: number;
    date: Date;
    isReaded: boolean;
}