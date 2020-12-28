import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ClientService implements Resolve<any>
{
    onClientsChanged: BehaviorSubject<any>;
    clients: any[];

    onClientChanged: BehaviorSubject<any>;
    client: {};

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onClientsChanged = new BehaviorSubject({});
        this.clients = [];

        this.onClientChanged = new BehaviorSubject({});
        this.client = {};
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
            .get(`${this.baseAdddress}/clients/${projectId}`)
            .subscribe((response: any) => {
                this.client = response;
                this.onClientChanged.next(this.client);
                resolve(response);
            });
        });
    }

    loadAll(filter){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/clients/list?razaoSocial=${filter}`)
            .subscribe((response: any) => {
                this.clients = response;
                this.onClientsChanged.next(this.clients);
                resolve(response);
            });
        });
    }

    save(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .post(`${this.baseAdddress}/clients/save`, entity)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }
} 