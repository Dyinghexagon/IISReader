import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class BaseService {

    constructor(
        protected http: HttpClient,
        protected zone: NgZone
    ) { }

    private getHeaders(): HttpHeaders {
        return new HttpHeaders(
            {
                "content-type": "application/json", 
                "cache-control": "no-cache",
                "Accept": "application/json",
                "Authorization": "Bearer " + this.getJwtToken()
            }
        );
    }

    public get tokenKey(): string {
        return "accessToken";
    }

    public post(url: string, data: any, silent?: boolean): Promise<any> {
        const observable = this.http.post(url, JSON.stringify(data), { headers: this.getHeaders(), observe: "response", withCredentials: true  });
        return this.subscribe(observable, url, silent);
    }

    public get(url: string, silent?: boolean, full: boolean = false): Promise<any> {
        const observable = this.http.get(url, { headers: this.getHeaders(), observe: "response", withCredentials: true });
        return this.subscribe(observable, url, silent, full);
    }

    public put(url: string, data: any, silent?: boolean): Promise<any> {
        const observable = this.http.put(url, JSON.stringify(data), { headers: this.getHeaders(), observe: "response", withCredentials: true });
        return this.subscribe(observable, url, silent);
    }

    public delete(url: string, silent?: boolean): Promise<any> {
        const observable = this.http.delete(url, { headers: this.getHeaders(), observe: "response" });
        return this.subscribe(observable, url, silent);
    }

    protected subscribe(observable: Observable<object>, url: string, silent?: boolean, full: boolean = false): Promise<any> {
        const promise = new Promise((resolve, reject) => {
            observable.subscribe({
                next: (r: any) => {
                    setTimeout(() => {
                        this.zone.run(() => {
                            resolve(r);
                        });
                    });
                },
                error: r => {
                    if (silent) {   // ToDo check
                        if (r.status === 500) {
                            resolve({ code: "500" });
                        } else {
                            resolve(r.error || null);
                        }
                    } else {
                        if (r.status === 401) {
                            reject(r);
                            return;
                        }
                        if (r.status === 403) {
                            resolve(null);
                        }

                        resolve(r.error || null);
                    }
                }
            });
        });

        return promise;
    }

    public getJwtToken(): string | null {
        return localStorage.getItem(this.tokenKey) ?? null;
    }
}