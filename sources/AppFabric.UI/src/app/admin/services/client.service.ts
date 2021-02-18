import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ResponseData } from '../models/ResponseData';

@Injectable({
    providedIn: 'root',
})
export class ClientService implements Resolve<any>
{


    onClientsChanged: BehaviorSubject<any>;
    clients: any[];

    onClientChanged: BehaviorSubject<any>;
    client: {};

    onClientLoaded: BehaviorSubject<any>;

    onClientUpdateError: BehaviorSubject<any>;
    onClientDeleted: BehaviorSubject<any>;

    baseAdddress: string = "https://localhost:5001/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onClientsChanged = new BehaviorSubject({});
        this.clients = [];

        this.onClientChanged = new BehaviorSubject({});
        this.client = {};

        this.onClientUpdateError = new BehaviorSubject({});
        this.onClientDeleted = new BehaviorSubject({});
        this.onClientLoaded = new BehaviorSubject({});
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
                if(response.isSucceed){
                    this.client = response.data;
                    this.onClientLoaded.next(this.client);
                }
                resolve(response);
            });
        });
    }

    delete(clientId){
        return new Promise((resolve, reject) => {
            this._httpClient
            .delete(`${this.baseAdddress}/clients/${clientId}`)
            .subscribe((response: any) => {
                this.client = {};
                this.onClientDeleted.next({executed: true});
                resolve(response);
            });
        });
    }

    loadAll(filter){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/clients/list?name=${filter}`)
            .subscribe((response: ResponseData) => {
                if(response.isSucceed){
                    this.clients = response.data;
                    this.onClientsChanged.next(this.clients);
                }
                resolve(response);
            });
        });
    }

    save(entity){
        return new Promise((resolve, reject) => {
            this._httpClient
            .post(`${this.baseAdddress}/clients/save`, entity)
            .subscribe((response: ResponseData) => {

                if(response.isSucceed){
                    this.client = entity;
                    this.onClientChanged.next(response);
                }else{
                    this.onClientUpdateError.next(response.violations.map(x => ({
                        propertyName: x.propertyName,
                        errorMessage: x.errorMessage,
                        message: x.errorMessage.replace("'Value'", x.propertyName).replace('.Value', '')
                    })));
                }
                
                resolve(response);
            });
        });
    }
} 