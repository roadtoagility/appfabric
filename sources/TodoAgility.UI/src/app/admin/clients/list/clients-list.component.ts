import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ClientService} from '../../services/client.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NbDialogService, NbToastrService } from '@nebular/theme';

import { NewClientFormComponent } from '../new/new-client-modal.component';
import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';

import {ClientData} from '../../models/entities/ClientData';
import { DialogDeleteComponent } from '../../common/modals/delete/dialog-delete.component';



@Component({
  selector: 'ngx-listar-clientes',
  templateUrl: './clients-list.component.html',
  styleUrls: ['./clients-list.component.scss']
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

  constructor(private _toastrService: NbToastrService, private _formBuilder: FormBuilder, private service: SmartTableData, private _clientService: ClientService, private _router: Router, private dialogService: NbDialogService) {
    const data = this.service.getClients();
    //this.source.load(data);

    this._unsubscribeAll = new Subject();
    
    this._clientService.onClientsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entities = response;
      }else{
        this.entities = [];
      }

      this.source.load(this.entities);
    });

    this._clientService.onClientDeleted
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(isDeleted => {
      if(Object.keys(isDeleted).length > 0){
        if(isDeleted.executed){
          this._clientService.loadAll("");
          this._toastrService.show("Client deleted!", "Action", { status: "danger", icon: "trash-2-outline" });
        }
      }
    });

    this._clientService.loadAll("");
  }

  onEdit(event): void{
    this._router.navigateByUrl('/admin/editar-cliente/' + event.data.id);
  }

  onDeleteConfirm(register): void {
    console.log(register.data);
    this.dialogService.open(DialogDeleteComponent, {
      context: {
        message: `Do you want to delete client ${register.data.name}?`
      },
    })
    .onClose.subscribe(clientConfirmed => this.onDeleteConfirmed(register.data, clientConfirmed));
  }

  onDeleteConfirmed(client, deleteConfirmed){
    if(deleteConfirmed){
      this._clientService.delete(client.id);
    }
  }

  buildForm(client){
    this.form = this._formBuilder.group({
      name: [client.name, Validators.required],
      cnpj: [client.cnpj, Validators.required],
      commercialEmail: [client.email, Validators.required]
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
    .onClose.subscribe(project => this._clientService.loadAll(""));
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
