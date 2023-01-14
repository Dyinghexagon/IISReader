import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable } from "rxjs";
import { AppConfig } from "../app.config";
import { UserModel } from "../components/models/user.model";
import { BaseService } from "./base.service";

@Injectable()
export class UserService extends BaseService {
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
        return this.post(`${this.config.accountApi}`, user);
    }

}