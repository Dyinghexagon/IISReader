
export interface IAlert {
    type: AlertType;
    message: string;
    title: string;
    date: Date;
}

export type AlertType = "success" | "error";