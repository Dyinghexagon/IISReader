import { Injectable } from "@angular/core";
import { AccountsService } from "src/app/services/accounts.service";
import { AuthService } from "src/app/services/auth.service";
import { AccountModel } from "./account.model";
import { AuthState } from "./auth-state.module";

@Injectable()
export class AppState {
    public readonly authState: AuthState;

    constructor(
        private readonly accountService: AccountsService,
        private readonly authService: AuthService
    ) {
        this.authState = new AuthState();
    }

    public async getAccount(): Promise<AccountModel | null>{
        const login = localStorage.getItem(this.authService.loginKey);
        if (login) {
            const account = await this.accountService.getAccount(login);
            return account;
        }
        return null;
    }

    public getJwtToken(): string | null {
        return localStorage.getItem(this.authService.tokenKey) ?? null;
    }

}