import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ResponseData } from 'app/admin/models/ResponseData';

@Injectable({
    providedIn: 'root',
})
export class ClientModalService implements Resolve<any>
{
    onClientsChanged: BehaviorSubject<any>;
    clients: any[];

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onClientsChanged = new BehaviorSubject({});
        this.clients = [];
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

    search(filter){
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
} 