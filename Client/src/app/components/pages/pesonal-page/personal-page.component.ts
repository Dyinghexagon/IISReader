import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/authentication.service";

@Component({
    selector: "personal-page",
    templateUrl: "./personal-page.component.html",
    styleUrls: [ "./personal-page.component.scss" ]
})

export class PersonalPageComponent {

    constructor(
        private readonly authenticationService: AuthenticationService,
        private readonly router: Router,

        ) {
    }

    public logout(): void {
        this.authenticationService.logout();
        this.router.navigate(["/"]);
    }
}
