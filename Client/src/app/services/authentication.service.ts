import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { UserModel } from "../components/models/user.model";
import { BaseService } from "./base.service";

@Injectable()
export class AuthenticationService extends BaseService {

    constructor(
        http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public login(user: UserModel): Promise<number> {
        return this.post(`${this.config.accountApi}/authenticate`, user).then((token: Response) => {
            if (token) {
                localStorage.setItem("currentAccount", JSON.stringify(user));
            }
            return !token ? 404 : token.status;
        });
    }

    public logout() {
        localStorage.removeItem("currentAccount");
    }
}