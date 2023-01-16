import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: "security-page",
    templateUrl: "./security-page.component.html",
    styleUrls: ["./security-page.component.scss"]
})

export class SecurityPageComponent implements OnInit {

    public secid?: string;

    constructor(private route: ActivatedRoute,) {
    }

    public ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            this.secid = params["secid"];
        });
    }

}