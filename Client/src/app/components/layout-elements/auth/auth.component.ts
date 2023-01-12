import { Component } from "@angular/core";
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { UserService } from "src/app/services/user.services";
import { UserModel } from "../../models/user.model";

@Component({
    selector: "auth",
    templateUrl: "./auth.component.html",
    styleUrls: ["./auth.component.scss"]
})

export class AuthComponent {

    public tab: "login" | "reg" = "login";
    public loginForm: FormGroup;
    public regForm: FormGroup;
    public hidePasswordLoginForm: boolean = true;
    public hidePasswordRegForm: boolean = true;
    public hideRepitPasswordRegForm: boolean = true;
    
    private readonly regexp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$/;

    constructor(private readonly userService: UserService) {
        this.loginForm = this.initLoginForm();
        this.regForm = this.initRegForm();
    }

    private initLoginForm(): FormGroup {
        return new FormGroup({
            email: new FormControl("", [Validators.required, Validators.email]),
            password: new FormControl("", [Validators.required, Validators.pattern(this.regexp)]),
            loginCheck: new FormControl("")
        });
    }

    private initRegForm(): FormGroup {
        return new FormGroup({
            regLogin: new FormControl("", [Validators.required, Validators.minLength(8), Validators.maxLength(12)]),
            regEmail: new FormControl("", [Validators.required, Validators.email]),
            regPassword: new FormControl("", [Validators.required, Validators.pattern(this.regexp)]),
            repitPassword: new FormControl("", [Validators.required, Validators.pattern(this.regexp)])
        }, {validators: this.checkPasswords});
    }

    private checkPasswords: ValidatorFn = (group: AbstractControl):  ValidationErrors | null => { 
        let pass = group.get("regPassword")?.value;
        let confirmPass = group.get('repitPassword')?.value

        return pass === confirmPass ? null : { notSame: true }
    }

    public get loginPassword(): string {
        return this.loginForm.get("password")?.value;
    }

    public get regRepitPassword(): string {
        return this.regForm.get("repitPassword")?.value;
    }

    public get regPassword(): string {
        return this.regForm.get("regPassword")?.value;
    }

    public submitLoginForm(): void {
        //
    }
    
    public submitRegForm(): void {
        const newUser = this.regForm.value as UserModel;
        this.userService.createUser(newUser);
    }

    public hidePassword(id: string): void {
        const password = document.getElementById(id) as HTMLInputElement;
        if (password.type === "password") {
            password.type = "text";
        } else {
            password.type = "password";
        }
    }

    public test(): void {
        console.warn(this.userService.user);
        this.userService.getUsers().subscribe(users => {
            console.warn(users);
        },error => {
            console.error(error);
        })
    }

}