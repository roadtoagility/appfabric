import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
})
export class ReleaseModalService implements Resolve<any>
{
    onReleasesChanged: BehaviorSubject<any>;
    releases: any[];

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onReleasesChanged = new BehaviorSubject({});
        this.releases = [];
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

    search(filter, projectId){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/releases/list/${projectId}/${filter}`)
            .subscribe((response: any) => {
                this.releases = response;
                this.onReleasesChanged.next(this.releases);
                resolve(response);
            });
        });
    }
} 