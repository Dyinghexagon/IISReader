export class UserModel {
    
    public id: string;
    public login: string;
    public email: string;
    public password: string;

    constructor(data: IUserModel) {
        this.id = data.id;
        this.login = data.login;
        this.email = data.email;
        this.password = data.password;
    }

}

export interface IUserModel {
    id: string;
    login: string;
    email: string;
    password: string;
}