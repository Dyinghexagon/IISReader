import { Component, OnInit } from "@angular/core";
import { AbstractControl, UntypedFormControl, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Guid } from "guid-typescript";
import { AccountService } from "src/app/services/account.service";
import { AlertService } from "src/app/services/alert.service";
import { AuthenticationService } from "src/app/services/authentication.service";
import { AppState } from "../../models/app-state.module";
import { IAccountModel, AccountModel } from "../../models/account.model";

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
    
    private readonly REGEXP = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$/;
    private readonly MIN_LENGHT_LOGIN = 4;
    private readonly MAX_LENGHT_LOGIN = 12;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly router: Router,
        private readonly userService: AccountService,
        private readonly alertService: AlertService,
        private readonly authenticationService: AuthenticationService,
        private readonly appState: AppState
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
            login: new UntypedFormControl("", [Validators.minLength(this.MIN_LENGHT_LOGIN), Validators.maxLength(this.MAX_LENGHT_LOGIN)]),
            password: new UntypedFormControl("", [Validators.required, Validators.pattern(this.REGEXP)]),
            loginCheck: new UntypedFormControl("")
        });
    }

    private initRegForm(): UntypedFormGroup {
        return new UntypedFormGroup({
            regLogin: new UntypedFormControl("", [Validators.required, Validators.minLength(this.MIN_LENGHT_LOGIN), Validators.maxLength(this.MAX_LENGHT_LOGIN)]),
            regPassword: new UntypedFormControl("", [Validators.required, Validators.pattern(this.REGEXP)]),
            repitPassword: new UntypedFormControl("", [Validators.required, Validators.pattern(this.REGEXP)])
        }, {validators: this.checkPasswords});
    }

    private checkPasswords: ValidatorFn = (group: AbstractControl):  ValidationErrors | null => { 
        let pass = group.get("regPassword")?.value;
        let confirmPass = group.get('repitPassword')?.value

        return pass === confirmPass ? null : { notSame: true }
    }

    public async submitLoginForm(): Promise<void> {
        const statusLogin = await this.authenticationService.login(new AccountModel({
            id: Guid.create().toString(),
            login: this.login?.value,
            password: this.loginPassword?.value
        }));

        this.createAlertAndNavigate(statusLogin, "Авторизация прошла успешно!", "Ошибка авторизации!")
    }
    
    public async submitRegForm(): Promise<void> {
        if (this.regForm.valid) {
            let newUser = new AccountModel(
                {
                    id: Guid.create().toString(),
                    login: this.regLogin?.value,
                    password: this.regPassword?.value
                } as IAccountModel
            );

            const response = await this.userService.createUser(newUser);
            console.warn(response);
            const status = response.status ?? 200;
            this.createAlertAndNavigate(status, "Регистрация прошла успещно", response.error);
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

    private createAlertAndNavigate(statusResponse: number, messangeSuccess: string, messangeError: string): void {
        if(statusResponse === 200) {
            this.alertService.success(messangeSuccess);
            this.router.navigate(["/personal-page"]);
            this.appState.authState.login$.next();
        } else {
            this.alertService.error(messangeError);
        }
    }
    
    public get login(): AbstractControl | null {
        return this.loginForm.get("login");
    }

    public get loginPassword(): AbstractControl | null {
        return this.loginForm.get("password");
    }

    public get regLogin(): AbstractControl | null {
        return this.regForm.get("regLogin");
    }

    public get regPassword(): AbstractControl | null {
        return this.regForm.get("regPassword");
    }

    public get regRepitPassword(): AbstractControl | null {
        return this.regForm.get("repitPassword");
    }

}