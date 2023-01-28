import { Subject } from "rxjs";

export class AuthState {
    public readonly login$ = new Subject<void>();
}