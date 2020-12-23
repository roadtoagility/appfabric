import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ActivityService implements Resolve<any>
{
    onActivitiesChanged: BehaviorSubject<any>;
    activities: any[];

    onActivitiyChanged: BehaviorSubject<any>;
    activitiy: {};

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onActivitiesChanged = new BehaviorSubject({});
        this.activities = [];

        this.onActivitiyChanged = new BehaviorSubject({});
        this.activitiy = {};
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

    load(projectId){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/Activities/${projectId}`)
            .subscribe((response: any) => {
                this.activitiy = response;
                this.onActivitiyChanged.next(this.activitiy);
                resolve(response);
            });
        });
    }

    loadAll(filter){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/Activities/list?titulo=${filter}`)
            .subscribe((response: any) => {
                this.activities = response;
                this.onActivitiesChanged.next(this.activities);
                resolve(response);
            });
        });
    }

    save(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .post(`${this.baseAdddress}/Activities/save`, entity)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }
} 