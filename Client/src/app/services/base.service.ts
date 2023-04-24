import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class BaseService {

    constructor(protected http: HttpClient) { }

    private get headers(): HttpHeaders {
        return new HttpHeaders({ "content-type": "application/json", "cache-control": "no-cache" });
    }

    protected post(url: string, data: any, headers: HttpHeaders = this.headers): Promise<any> {
        let res = this.http.post(url, data, { headers: headers ?? this.headers });
        return res.toPromise().then(data => {
            return data;
        }).catch(err => err);
    }

    protected get(url: string): Promise<any> {
        let res = this.http.get(url);
        return res.toPromise().then(data => {
            return data;
        }).catch(err => err);
    }

    protected put(url: string, data: any, headers: HttpHeaders = this.headers): Promise<any> {
        let res = this.http.put(url, data, { headers: headers ?? this.headers });
        return res.toPromise().then(data => {
            return data;
        }).catch(err => err);
    }
}