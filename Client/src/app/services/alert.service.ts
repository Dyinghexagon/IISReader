import { Injectable } from "@angular/core";
import { NavigationStart, Router } from "@angular/router";
import { Observable, Subject } from "rxjs";
import { IAlert } from "../components/models/alert.model";

@Injectable()
export class AlertService {
    private alert = new Subject<IAlert>();
    private keepAfterNavigationChange = true;

    constructor(private router: Router) {
        this.router.events.subscribe(event => {
            if (event instanceof NavigationStart)
                if (this.keepAfterNavigationChange) {
                    this.keepAfterNavigationChange = false;
                }
        })
    }
    
    public get message(): Observable<IAlert> {
        return this.alert.asObservable();
    }

    public createAllert(status: number, successMessange: string, errorMessage: string): void {
        if (status === 200) {
            this.success(successMessange);
        } else {
            this.error(errorMessage);
        }
    }

    private success(message: string, keepAfterNavigationChange = false): void {
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.alert.next({type: "success", message: message});
    }

    private error(message: string, keepAfterNavigationChange = false): void {
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.alert.next({type: "error", message: message});
    }

}