import { Injectable } from "@angular/core";
import { AuthState } from "./auth-state.module";

@Injectable()
export class AppState {
    public readonly authState: AuthState;

    constructor() {
        this.authState = new AuthState();
    }
}