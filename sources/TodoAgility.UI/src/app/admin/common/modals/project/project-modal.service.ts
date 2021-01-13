import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
})
export class ProjectModalService implements Resolve<any>
{
    onProjectsChanged: BehaviorSubject<any>;
    projects: any[];

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onProjectsChanged = new BehaviorSubject({});
        this.projects = [];
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

    search(filter, idCliente){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/projects/list/${idCliente}/${filter}`)
            .subscribe((response: any) => {
                this.projects = response;
                this.onProjectsChanged.next(this.projects);
                resolve(response);
            });
        });
    }
} 