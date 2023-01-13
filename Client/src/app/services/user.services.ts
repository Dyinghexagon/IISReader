import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { AppConfig } from "../app.config";
import { UserModel } from "../components/models/user.model";
import { BaseService } from "./base.service";

@Injectable()
export class UserService extends BaseService {
    public user?: UserModel;

    constructor(
        protected http: HttpClient,
        protected config: AppConfig
    ) {
        super(http);
    }

    public getUser(id: string): Observable<UserModel> {
        return this.http.get<UserModel>(`${this.config.accountApi}}/GetAccount/${id}`);
    }

    public getUsers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>(`${this.config.accountApi}}/GetAccounts`);
    }

    public createUser(user: UserModel): Observable<any> {
        return this.http.post<UserModel>(`${this.config.accountApi}`, user, {headers: this.headers}).pipe(catchError(this.handleError));
    }

}