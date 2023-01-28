import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { AccountModel } from "../components/models/account.model";
import { BaseService } from "./base.service";

@Injectable()
export class AuthenticationService extends BaseService {

    constructor(
        http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public login(user: AccountModel): Promise<number> {
        return this.post(`${this.config.accountApi}/authenticate`, user).then((token: Response) => {
            if (token) {
                localStorage.setItem("currentAccount", JSON.stringify({id: user.id, login: user.login}));
            }
            return !token ? 404 : token.status;
        });
    }

    public logout() {
        localStorage.removeItem("currentAccount");
    }
}