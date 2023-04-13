import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";
import { StockService } from "src/app/services/stock.service";
import { AppState } from "../../models/app-state.module";

@Component({
    selector: "personal-page",
    templateUrl: "./personal-page.component.html",
    styleUrls: [ "./personal-page.component.scss" ]
})

export class PersonalPageComponent {

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly appState: AppState,
        private readonly stockService: StockService
        ) {
    }

    public logout(): void {
        this.authService.logout();
        this.router.navigate(["/"]);
        this.appState.authState.logout$.next();
    }

    public fillingStock(): void {
        this.stockService.fillingStocks();
    }
}
