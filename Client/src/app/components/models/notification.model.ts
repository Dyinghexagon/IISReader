export class NotificationModel {
    private _title: string;
    private _description: string;
    private _secId: string;
    private _volume: number;
    private _date: Date;
    private _isReaded: boolean;

    constructor(data: INotificationModel) {
        this._title = data.title;
        this._description = data.description;
        this._secId = data.secId;
        this._volume = data.volume;
        this._date = data.date;
        this._isReaded = data.isReaded;
    }

    public get Title(): string {
        return this._title;
    }

    public get Description(): string {
        return this._description;
    }

    public get SecId(): string {
        return this._secId;
    }

    public get Volume(): number {
        return this._volume;
    }

    public get Date(): Date {
        return this._date;
    }

    public get IsReaded(): boolean {
        return this._isReaded;
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