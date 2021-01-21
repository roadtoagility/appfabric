import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ClientService} from '../../services/client.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NbDialogService } from '@nebular/theme';

import { NewClientFormComponent } from '../new/new-client-modal.component';
import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';

import {ClientData} from '../../models/entities/ClientData';

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
      columnTitle: 'Actions'
    },
    mode:'external',
    columns: {
      id: {
        title: 'ID',
        type: 'number',
        editable: false,
        filter: false
      },
      name: {
        title: 'Company Name',
        type: 'string',
        filter: false,
      },
      cnpj: {
        title: 'Cnpj',
        type: 'string',
        filter: false
      },
      commercialEmail: {
        title: 'Commercial Email',
        type: 'string',
        filter: false
      }
    },
  };

  source: LocalDataSource = new LocalDataSource();
  entities: any = [];

  statusForm: FormGroup;
  form: FormGroup;

  constructor(private _formBuilder: FormBuilder, private service: SmartTableData, private _clientService: ClientService, private _router: Router, private dialogService: NbDialogService) {
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
    this._router.navigateByUrl('/admin/editar-cliente/' + event.data.id);
  }

  onDeleteConfirm(event): void {
    console.log(event);
  }

  buildForm(client){
    this.form = this._formBuilder.group({
      name: [client.name, Validators.required],
      cnpj: [client.cnpj, Validators.required],
      email: [client.email, Validators.required]
    });
  }

  new(){
    this.buildForm(new ClientData({}));
    this.dialogService.open(NewClientFormComponent, {
      context: {
        title: 'New Client',
        form: this.form
      },
    })
    .onClose.subscribe(project => console.log(project));
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
