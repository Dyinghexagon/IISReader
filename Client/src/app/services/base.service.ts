import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";

@Injectable()
export class BaseService {

    constructor(http: HttpClient) {
    }

    handleError(error: HttpErrorResponse) {
        return throwError(error);
    }

    protected get headers(): HttpHeaders {
        return new HttpHeaders({ "content-type": "application/json", "cache-control": "no-cache" });
    }
}