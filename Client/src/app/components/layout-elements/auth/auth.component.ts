import { Component } from "@angular/core";
import { AbstractControl, UntypedFormControl, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { UserService } from "src/app/services/user.services";
import { IUserModel, UserModel } from "../../models/user.model";

@Component({
    selector: "auth",
    templateUrl: "./auth.component.html",
    styleUrls: ["./auth.component.scss"]
})

export class AuthComponent {

    public tab: "login" | "reg" = "login";
    public loginForm: UntypedFormGroup;
    public regForm: UntypedFormGroup;
    public hidePasswordLoginForm: boolean = true;
    public hidePasswordRegForm: boolean = true;
    public hideRepitPasswordRegForm: boolean = true;
    
    private readonly regexp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$/;

    constructor(private readonly userService: UserService) {
        this.loginForm = this.initLoginForm();
        this.regForm = this.initRegForm();
    }

    private initLoginForm(): UntypedFormGroup {
        return new UntypedFormGroup({
            email: new UntypedFormControl("", [Validators.required, Validators.email]),
            password: new UntypedFormControl("", [Validators.required, Validators.pattern(this.regexp)]),
            loginCheck: new UntypedFormControl("")
        });
    }

    private initRegForm(): UntypedFormGroup {
        return new UntypedFormGroup({
            regLogin: new UntypedFormControl("", [Validators.required, Validators.minLength(8), Validators.maxLength(12)]),
            regEmail: new UntypedFormControl("", [Validators.required, Validators.email]),
            regPassword: new UntypedFormControl("", [Validators.required, Validators.pattern(this.regexp)]),
            repitPassword: new UntypedFormControl("", [Validators.required, Validators.pattern(this.regexp)])
        }, {validators: this.checkPasswords});
    }

    private checkPasswords: ValidatorFn = (group: AbstractControl):  ValidationErrors | null => { 
        let pass = group.get("regPassword")?.value;
        let confirmPass = group.get('repitPassword')?.value

        return pass === confirmPass ? null : { notSame: true }
    }

    public submitLoginForm(): void {
        //
    }
    
    public async submitRegForm(): Promise<void> {
        if (this.regForm.valid) {
            let newUser = new UserModel(
                {
                    id: Guid.create().toString(),
                    login: this.regForm.get("regLogin")?.value,
                    email: this.regForm.get("regEmail")?.value,
                    password: this.regForm.get("regPassword")?.value
                } as IUserModel
            );

            await this.userService.createUser(newUser);
        }
    }

    public hidePassword(id: string): void {
        const password = document.getElementById(id) as HTMLInputElement;
        if (password.type === "password") {
            password.type = "text";
        } else {
            password.type = "password";
        }
    }

    public get loginPassword(): AbstractControl | null {
        return this.loginForm.get("password");
    }

    public get loginEmail(): AbstractControl | null {
        return this.loginForm.get("email");
    }

    public get regLogin(): AbstractControl | null {
        return this.regForm.get("regLogin");
    }

    public get regEmail(): AbstractControl | null {
        return this.regForm.get("regEmail");
    }

    public get regPassword(): AbstractControl | null {
        return this.regForm.get("regPassword");
    }

    public get regRepitPassword(): AbstractControl | null {
        return this.regForm.get("repitPassword");
    }

}