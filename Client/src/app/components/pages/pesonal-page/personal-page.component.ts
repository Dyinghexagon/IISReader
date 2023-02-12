import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";
import { AppState } from "../../models/app-state.module";

@Component({
    selector: "personal-page",
    templateUrl: "./personal-page.component.html",
    styleUrls: [ "./personal-page.component.scss" ]
})

export class PersonalPageComponent {

    constructor(
        private readonly authenticationService: AuthService,
        private readonly router: Router,
        private readonly appState: AppState
        ) {
    }

    public logout(): void {
        this.authenticationService.logout();
        this.router.navigate(["/"]);
        this.appState.authState.logout$.next();
    }
}
