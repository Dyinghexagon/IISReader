import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subject } from "rxjs";
import { SecurityService } from "src/app/services/securitys.service";
import { SecurityModel } from "../../models/security.model";

@Component({
    selector: "securitys",
    templateUrl: "./securitys.component.html",
    styleUrls: [ "./securitys.component.scss" ]
})

export class SecuritysComponent implements OnInit, OnDestroy  {

    public securitys: SecurityModel[] = [];
    public date: string = "";
    public dtOptions: DataTables.Settings = {};
    public dtTrigger: Subject<any> = new Subject<SecurityModel[]>();
    
    constructor(public securityService: SecurityService) { }

    public async ngOnInit(): Promise<void> {
        this.dtOptions = {
            pagingType: 'full_numbers',
            pageLength: 10,
            processing: true
        };
        this.securitys = await this.securityService.getSecurityList();
        this.dtTrigger.next(this.securitys);
    }

    public ngOnDestroy(): void {
        this.dtTrigger.unsubscribe();
    }

}
