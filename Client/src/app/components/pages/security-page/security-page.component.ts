import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: "security-page",
    templateUrl: "./security-page.component.html",
    styleUrls: ["./security-page.component.scss"]
})

export class SecurityPageComponent implements OnInit {

    private _secid?: string;

    constructor(private route: ActivatedRoute,) {
    }

    public ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            this._secid = params["secid"];
        });
    }

}

//http://iss.moex.com/iss/engines/stock/markets/shares/securities/SBER/candles.json?from=2021-01-01