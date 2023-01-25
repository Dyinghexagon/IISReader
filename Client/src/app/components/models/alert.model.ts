
export interface IAlert {
    type: AlertType;
    message: string;
}

export type AlertType = "success" | "error";