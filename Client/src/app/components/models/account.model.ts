export class AccountModel {
    
    public id: string;
    public login: string;
    public password: string;

    constructor(data: IAccountModel) {
        this.id = data.id;
        this.login = data.login;
        this.password = data.password;
    }

}

export interface IAccountModel {
    id: string;
    login: string;
    password: string;
}