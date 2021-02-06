import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
})
export class ActivityModalService implements Resolve<any>
{
    onActivitiesChanged: BehaviorSubject<any>;
    activities: any[];

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onActivitiesChanged = new BehaviorSubject({});
        this.activities = [];
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

    search(title, projectId){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/activities/list/${projectId}/${title}`)
            .subscribe((response: any) => {
                this.activities = response;
                this.onActivitiesChanged.next(this.activities);
                resolve(response);
            });
        });
    }
} 