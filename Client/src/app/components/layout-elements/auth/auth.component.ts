import { Component, OnInit } from "@angular/core";
import { AbstractControl, UntypedFormControl, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { Guid } from "guid-typescript";
import { AccountService } from "src/app/services/account.service";
import { AlertService } from "src/app/services/alert.service";
import { AuthenticationService } from "src/app/services/authentication.service";
import { IUserModel, UserModel } from "../../models/user.model";

@Component({
    selector: "auth",
    templateUrl: "./auth.component.html",
    styleUrls: ["./auth.component.scss"]
})

export class AuthComponent implements OnInit {

    public tab: "login" | "reg" = "login";
    public loginForm: UntypedFormGroup;
    public regForm: UntypedFormGroup;
    public hidePasswordLoginForm: boolean = true;
    public hidePasswordRegForm: boolean = true;
    public hideRepitPasswordRegForm: boolean = true;
    
    private returnUrl: string = "/";
    
    private readonly regexp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$/;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly userService: AccountService,
        private readonly alertService: AlertService,
        private readonly authenticationService: AuthenticationService
    ) {
        this.loginForm = this.initLoginForm();
        this.regForm = this.initRegForm();
    }

    public ngOnInit(): void {
        this.authenticationService.logout();
        this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
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
        this.authenticationService.login(new UserModel({
            id: Guid.create().toString(),
            login: this.loginEmail?.value,
            email: this.loginEmail?.value,
            password: this.loginPassword?.value
        }));
    }
    
    public async submitRegForm(): Promise<void> {
        if (this.regForm.valid) {
            let newUser = new UserModel(
                {
                    id: Guid.create().toString(),
                    login: this.regLogin?.value,
                    email: this.regEmail?.value,
                    password: this.regPassword?.value
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