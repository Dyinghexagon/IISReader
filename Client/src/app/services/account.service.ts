import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app.config";
import { UserModel } from "../components/models/user.model";
import { BaseService } from "./base.service";

@Injectable()
export class AccountService extends BaseService {
    public user?: UserModel;

    constructor(
        http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public getUser(id: string): Promise<UserModel> {
        return this.get(`${this.config.accountApi}}/GetAccount/${id}`);
    }

    public getUsers(): Promise<UserModel[]> {
        return this.get(`${this.config.accountApi}}/GetAccounts`);
    }

    public createUser(user: UserModel): Promise<UserModel> {
        return this.post(`${this.config.accountApi}/register`, user, this.jwt());
    }

    private jwt() {
        console.warn(localStorage);

        if (!localStorage.length) return new HttpHeaders({ "content-type": "application/json", "cache-control": "no-cache" });
        let token = JSON.parse(localStorage.getItem("currentAccount") ?? "").token;
        return new HttpHeaders({
            "Accept":"application/json",
            "Authorization": "Bearer " + token
        });
    }

}