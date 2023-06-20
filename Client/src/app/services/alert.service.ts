import { Injectable } from "@angular/core";
import { MdbModalRef, MdbModalService } from "mdb-angular-ui-kit/modal";
import { AlertModalComponent } from "../components/shared/alert/alert.component";

@Injectable()
export class AlertService {
    public alertRef: MdbModalRef<AlertModalComponent> | null = null;

    constructor(private readonly modalService: MdbModalService) {}

    public createAllert(status: number, title: string, successMessange: string, errorMessage: string): void {
        const type = status === 200 ? "success" : "error";
        const message = status === 200 ? successMessange : errorMessage;
        this.alertRef = this.modalService.open(AlertModalComponent, {
            data: {
                alert: {
                    type: type, 
                    message: message,
                    title: title,
                    date: new Date()
                }
            }
        });
        setTimeout(() => this.alertRef?.close(), 5 * 1000);
    }

}