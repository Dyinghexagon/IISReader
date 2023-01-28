import { Injectable } from "@angular/core";
import { AccountService } from "src/app/services/account.service";
import { AccountModel } from "./account.model";
import { AuthState } from "./auth-state.module";

@Injectable()
export class AppState {
    public readonly authState: AuthState;

    constructor(
        private readonly accountService: AccountService
    ) {
        this.authState = new AuthState();
    }

    public async getAccount(): Promise<AccountModel | null>{
        const accountRaw = localStorage.getItem("currentAccount");
        if (accountRaw) {
            const login = JSON.parse(accountRaw)["login"];
            const account = await this.accountService.getAccount(login);
            return account;
        }
        return null;
    }

}