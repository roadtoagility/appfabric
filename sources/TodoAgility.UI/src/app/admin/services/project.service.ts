import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ResponseData } from '../models/ResponseData';

@Injectable({
    providedIn: 'root',
})
export class ProjectService implements Resolve<any>
{
    onProjectsChanged: BehaviorSubject<any>;
    projects: any[];

    onProjectChanged: BehaviorSubject<any>;
    project: {};

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onProjectsChanged = new BehaviorSubject({});
        this.projects = [];

        this.onProjectChanged = new BehaviorSubject({});
        this.project = {};
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
            .get(`${this.baseAdddress}/projects/${projectId}`)
            .subscribe((response: any) => {
                this.project = response;
                this.onProjectChanged.next(this.project);
                resolve(response);
            });
        });
    }

    loadAll(filter){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/projects/list?nome=${filter}`)
            .subscribe((response: ResponseData) => {
                this.projects = response.items;
                this.onProjectsChanged.next(this.projects);
                resolve(response);
            });
        });
    }

    save(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .post(`${this.baseAdddress}/projects/save`, entity)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }
} 