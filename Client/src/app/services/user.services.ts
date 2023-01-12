import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AppConfig } from "../app.config";
import { UserModel } from "../components/models/user.model";

@Injectable()
export class UserService {
    public user?: UserModel;

    constructor(
        protected http: HttpClient,
        protected config: AppConfig,
    ) {
        this.http.get<UserModel>('/userApi/GetUser').subscribe(result => {
            this.user = result;
          }, error => console.error(error));
    }

    public getUsers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>('/userApi/GetUsers');
    }

    public createUser(user: UserModel): void {
        this.http.post<UserModel>("/userApi/CreateUser", user).subscribe(data => {
            console.warn("Create user!");
            console.warn(data);
        });
    }

}