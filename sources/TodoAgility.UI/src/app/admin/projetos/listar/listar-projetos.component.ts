import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ProjectService} from '../../services/project.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NbDialogService } from '@nebular/theme';

import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';

import { NewProjectFormComponent } from '../new/new-project-modal.component';

import {Project} from '../../models/entities/Project';

@Component({
  selector: 'ngx-listar-projetos',
  templateUrl: './listar-projetos.component.html',
  styleUrls: ['./listar-projetos.component.scss']
})
export class ListarProjetosComponent implements OnDestroy {

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
      cliente: {
        title: 'Cliente',
        type: 'string',
        filter: false
      },
      nome: {
        title: 'Nome do projeto',
        type: 'string',
        filter: false,
      },
      sigla: {
        title: 'Sigla',
        type: 'string',
        filter: false
      },
      gerente: {
        title: 'Gerente Responsável',
        type: 'string',
        filter: false
      },
      statusOrdemServico: {
        title: 'Status OS',
        type: 'string',
        filter: false
      },
    },
  };

  source: LocalDataSource = new LocalDataSource();
  entities: any = [];

  statusForm: FormGroup;
  form: FormGroup;

  constructor(private _formBuilder: FormBuilder, private service: SmartTableData, private _projetosService: ProjectService, private _router: Router, private dialogService: NbDialogService) {
   
    this._unsubscribeAll = new Subject();

    this._projetosService.onProjectsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entities = response;
        this.source.load(this.entities);
      }
    });
    
    this._projetosService.loadAll("");
  }

  onEdit(event): void{
    this._router.navigateByUrl('/admin/editar-projeto/' + event.data.id);
  }

  onDeleteConfirm(event): void {
    console.log(event);
  }

  buildForm(project){
    this.form = this._formBuilder.group({
      name: [project.name, Validators.required],
      code: [project.code, Validators.required],
      startDate: [project.startDate, Validators.required],
      budget: [project.budget, Validators.required],
      clientId: [project.clientId, Validators.required],
    });
  }

  new(){
    this.buildForm(new Project({}));
    this.dialogService.open(NewProjectFormComponent, {
      context: {
        title: 'New Project',
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
