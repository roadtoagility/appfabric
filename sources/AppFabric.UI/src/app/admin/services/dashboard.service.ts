import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DashboardService implements Resolve<any>
{
    onProjetosReleaseChanged: BehaviorSubject<any>;
    projetosRelease: any;

    onAtividadesConcluidasChanged:BehaviorSubject<any>;
    atividadesConcluidas: any[];

    onFaturamentoClientsChanged:BehaviorSubject<any>;
    faturamento: any[];

    onProjetosFavoritosChanged:BehaviorSubject<any>;
    projetosFavoritos: any[];

    baseAdddress: string = "https://localhost:44353/api";
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onProjetosReleaseChanged = new BehaviorSubject({});
        this.onAtividadesConcluidasChanged = new BehaviorSubject({});
        this.onFaturamentoClientsChanged = new BehaviorSubject({});
        this.onProjetosFavoritosChanged = new BehaviorSubject({});

        this.projetosRelease = {};
        this.atividadesConcluidas = [];
        this.faturamento = [];
        this.projetosFavoritos = [];
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

    loadProjetosReleases(){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/dashboard/ProjetosReleases`)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }

    loadAtividadesConcluidas(){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/dashboard/AtividadesConcluidas`)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }

    loadFaturamentoClients(){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/dashboard/FaturamentoClients`)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }

    loadProjetosFavoritos(){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`${this.baseAdddress}/dashboard/ProjetosFavoritos`)
            .subscribe((response: any) => {
                // this.projectActivities = response.items;
                // this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }
} 