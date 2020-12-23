import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ClientService} from '../../services/client.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'ngx-listar-clientes',
  templateUrl: './listar-clientes.component.html',
  styleUrls: ['./listar-clientes.component.scss']
})
export class ListarClientesComponent implements OnDestroy {

  private _unsubscribeAll: Subject<any>;

  settings = {
    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>'
    },
    actions: {
      edit: true,
      add: false,
      columnTitle: 'Ações'
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
      },
      cnpj: {
        title: 'Cnpj',
        type: 'string',
        filter: false
      },
      emailComercial: {
        title: 'E-mail Comercial',
        type: 'string',
        filter: false
      }
    },
  };

  source: LocalDataSource = new LocalDataSource();
  entities: any = [];

  constructor(private service: SmartTableData, private _clientService: ClientService) {
    const data = this.service.getClients();
    //this.source.load(data);

    this._unsubscribeAll = new Subject();
    
    this._clientService.onClientsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entities = response;
        this.source.load(this.entities);
      }
    });

    this._clientService.loadAll("");
  }

  onEdit(event): void{
    console.log(event);
  }

  onDeleteConfirm(event): void {
    console.log(event);
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
