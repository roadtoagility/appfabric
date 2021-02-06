import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
})
export class BillingService implements Resolve<any>
{
    onBillingsChanged: BehaviorSubject<any>;
    billngs: any[];

    onBillingChanged: BehaviorSubject<any>;
    billing: {};

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onBillingsChanged = new BehaviorSubject({});
        this.billngs = [];

        this.onBillingChanged = new BehaviorSubject({});
        this.billing = {};
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
        return new Promise((resolve, reject) => {

            Promise.all([
                
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }

    load(releaseId){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/billings/${releaseId}`)
            .subscribe((response: any) => {
                this.billing = response;
                this.onBillingChanged.next(this.billing);
                resolve(response);
            });
        });
    }

    loadAll(filter){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/billings/list?clientName=${filter}`)
            .subscribe((response: any) => {
                this.billngs = response;
                this.onBillingsChanged.next(this.billngs);
                resolve(response);
            });
        });
    }

    save(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .post(`${this.baseAdddress}/billings/save`, entity)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }
} 