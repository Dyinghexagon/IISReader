import { Component, OnInit } from "@angular/core";
import { SecurityService } from "src/app/services/securitys.service";
import { SecurityModel } from "../../models/security.model";

@Component({
    selector: "security",
    templateUrl: "./securitys-list.component.html",
    styleUrls: [ "./securitys-list.component.scss" ]
})

export class SecuritysListComponent implements OnInit  {

    public securitys: SecurityModel[] = [];

    constructor(public securityService: SecurityService) { }

    public async ngOnInit(): Promise<void> {
        this.securitys = await this.securityService.getSecurityList();
    }

}
