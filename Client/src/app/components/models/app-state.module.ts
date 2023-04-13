import { Injectable } from "@angular/core";
import { AccountsService } from "src/app/services/accounts.service";
import { AccountModel } from "./account.model";
import { AuthState } from "./auth-state.module";

@Injectable()
export class AppState {
    public readonly authState: AuthState;

    constructor(
        private readonly accountService: AccountsService
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