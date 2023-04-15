import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";
import { AccountModel } from "../../models/account.model";
import { AppState } from "../../models/app-state.module";

@Component({
    selector: "personal-page",
    templateUrl: "./personal-page.component.html",
    styleUrls: [ "./personal-page.component.scss" ]
})

export class PersonalPageComponent {

    public account: AccountModel | null | undefined;

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly appState: AppState
    ) { }

    public async ngOnInit(): Promise<void> {
        this.account = await this.appState.getAccount();
        console.warn(this.account);
    }

    public logout(): void {
        this.authService.logout();
        this.router.navigate(["/"]);
        this.appState.authState.logout$.next();
    }

}
