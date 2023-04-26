import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: "stock-page",
    templateUrl: "./stock-page.component.html",
    styleUrls: ["./stock-page.component.scss"]
})

export class StockPageComponent implements OnInit {

    public secid?: string;

    constructor(private route: ActivatedRoute) { }

    public ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            this.secid = params["secid"];
        });
    }

}