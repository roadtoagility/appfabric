import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ReleaseService implements Resolve<any>
{
    onReleasesChanged: BehaviorSubject<any>;
    releases: any[];

    onReleaseChanged: BehaviorSubject<any>;
    release: {};

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onReleasesChanged = new BehaviorSubject({});
        this.releases = [];

        this.onReleaseChanged = new BehaviorSubject({});
        this.release = {};
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
            .get(`${this.baseAdddress}/releases/${releaseId}`)
            .subscribe((response: any) => {
                this.release = response;
                this.onReleaseChanged.next(this.release);
                resolve(response);
            });
        });
    }

    loadAll(filter){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/releases/list?status=${filter}`)
            .subscribe((response: any) => {
                this.releases = response;
                this.onReleasesChanged.next(this.releases);
                resolve(response);
            });
        });
    }

    save(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .post(`${this.baseAdddress}/releases/save`, entity)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }
} 