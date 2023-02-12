import { Subject } from "rxjs";

export class AuthState {
    public readonly login$ = new Subject<void>();
    public readonly logout$ = new Subject<void>();
}