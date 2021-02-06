import { Component, Input, OnDestroy } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import {ClientModalService} from './client-modal.service';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  templateUrl: './client-modal.component.html',
  styleUrls: ['client-modal.component.scss'],
})
export class ClientSearchFormComponent implements OnDestroy {
  
    clientNameSearch: string;
    clients: any[];
    private _unsubscribeAll: Subject<any>;

    @Input() title: string;

    settings = {
        edit: {
          editButtonContent: '<i class="nb-arrow-thin-right"></i>'
        },
        actions: {
          edit: true,
          delete: false,
          add: false,
          columnTitle: 'Select'
        },
        mode:'external',
        columns: {
          id: {
            title: 'ID',
            type: 'number',
            editable: false,
            filter: false
          },
          razaoSocial: {
            title: 'Razão Social',
            type: 'string',
            filter: false,
          }
        },
      };

    source: LocalDataSource = new LocalDataSource();
  entities: any = [];

    constructor(private _clientService: ClientModalService, protected ref: NbDialogRef<ClientSearchFormComponent>) {
        this._unsubscribeAll = new Subject();
        this.clientNameSearch = "";

        this._clientService.onClientsChanged
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(response => {
        if(Object.keys(response).length > 0){
            this.entities = response;
            this.source.load(this.entities);
        }
        }); 
    }

    searchClient(){
        this._clientService.search(this.clientNameSearch);
    }

    onSelectClient(event): void{
        this.ref.close(event.data);
    }

    dismiss() {
      this.ref.close();
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
