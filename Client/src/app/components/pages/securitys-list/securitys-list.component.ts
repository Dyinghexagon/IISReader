import { Component } from "@angular/core";
import { SecurityService } from "src/app/services/securitys.service";
import { SecurityModel } from "../../models/security.model";

@Component({
    selector: "security",
    templateUrl: "./securitys-list.component.html",
    styleUrls: [ "./securitys-list.component.scss" ]
})

export class SecuritysListComponent {

    constructor(public readonly securityService: SecurityService) {
        securityService.getSecurityList().subscribe(res => {
            console.warn(res);
        }, error => {
            console.warn(error);
        })
    }

    public get securitys(): SecurityModel[]{
        return [
            {
                tiket: "GAZP",
                name: "Газпром",
                currentPrice: 100,
                changePerDay: 1
            },
            {
                tiket: "LKON",
                name: "Лукойл",
                currentPrice: 4077.5,
                changePerDay: -0.1
            },
            {
                tiket: "YNDX",
                name: "Яндекс",
                currentPrice: 1902.4,
                changePerDay: 0.24
            },
        ];
    }
}
