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
                } else {
                    this.alert.next({type: "error", message: "test!"});
                }
        })
    }
    
    public get message(): Observable<IAlert> {
        return this.alert.asObservable();
    }

    public createAlert(status: number, keepAfterNavigationChange = false): void {
        switch(status) {
            case 200: {
                console.warn("sucess!");
                this.success("sucess!", keepAfterNavigationChange);
                break;
            }
            case 404: {
                console.warn("error!");
                this.error("error!", keepAfterNavigationChange);
                break;
            }
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