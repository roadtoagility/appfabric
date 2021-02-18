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

    onProjectUpdateError: BehaviorSubject<any>;
    onProjectLoaded: BehaviorSubject<any>;

    onProjectDeleted: BehaviorSubject<any>;

    baseAdddress: string = "https://localhost:5001/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onProjectsChanged = new BehaviorSubject({});
        this.projects = [];

        this.onProjectChanged = new BehaviorSubject({});
        this.project = {};

        this.onProjectUpdateError = new BehaviorSubject({}); 
        this.onProjectDeleted = new BehaviorSubject({}); 
        this.onProjectLoaded = new BehaviorSubject({}); 
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
            .subscribe((response: ResponseData) => {
                if(response.isSucceed && response.data !== null){
                    this.project = response.data;
                    this.onProjectLoaded.next(this.project);
                }
                resolve(response);
            });
        });
    }

    loadAll(filter){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/projects/list?name=${filter}`)
            .subscribe((response: ResponseData) => {
                this.projects = response.data;
                this.onProjectsChanged.next(this.projects);
                resolve(response);
            });
        });
    }

    private handle(response, entity){
        if(response.isSucceed){
            this.project = entity;
            this.onProjectChanged.next(this.project);
        }else{
            this.onProjectUpdateError.next(response.violations.map(x => ({
                propertyName: x.propertyName,
                errorMessage: x.errorMessage,
                message: x.errorMessage.replace("'Value'", x.propertyName).replace('.Value', '')
            })));
        }
    }

    save(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .post(`${this.baseAdddress}/projects/save`, entity)
            .subscribe((response: ResponseData) => {
                if(response.isSucceed){
                    this.project = entity;
                    this.onProjectChanged.next(response);
                }else{
                    this.onProjectUpdateError.next(response.violations.map(x => ({
                        propertyName: x.propertyName,
                        errorMessage: x.errorMessage,
                        message: x.errorMessage.replace("'Value'", x.propertyName).replace('.Value', '')
                    })));
                }
            });
        });
    }

    update(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .put(`${this.baseAdddress}/projects/save/${entity.id}`, entity)
            .subscribe((response: ResponseData) => {
                this.handle(response, entity);
                resolve(response);
            });
        });
    }

    delete(projectId){
        return new Promise((resolve, reject) => {
            this._httpClient
            .delete(`${this.baseAdddress}/projects/${projectId}`)
            .subscribe((response: any) => {
                this.project = {};
                this.onProjectDeleted.next({executed: true});
                resolve(response);
            });
        });
    }
} 