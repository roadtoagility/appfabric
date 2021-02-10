import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ProjectService} from '../../services/project.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NbDialogService, NbToastrService } from '@nebular/theme';

import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';

import { NewProjectFormComponent } from '../new/new-project-modal.component';

import {Project} from '../../models/entities/Project';
import { DialogDeleteComponent } from '../../common/modals/delete/dialog-delete.component';

@Component({
  selector: 'ngx-listar-projetos',
  templateUrl: './projects-list.component.html',
  styleUrls: ['./projects-list.component.scss']
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
      clientName: {
        title: 'Client',
        type: 'string',
        filter: false
      },
      name: {
        title: 'Project Name',
        type: 'string',
        filter: false,
      },
      code: {
        title: 'Code',
        type: 'string',
        filter: false
      },
      owner: {
        title: 'Manager',
        type: 'string',
        filter: false
      },
      statusName: {
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

  formSearch: FormGroup;

  constructor(private _toastrService: NbToastrService, private _formBuilder: FormBuilder, private service: SmartTableData, private _projectService: ProjectService, private _router: Router, private dialogService: NbDialogService) {
   
    this._unsubscribeAll = new Subject();

    this._projectService.onProjectsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entities = response;
      }else{
        this.entities = [];
      }

      this.source.load(this.entities);
    });

    this._projectService.onProjectDeleted
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(isDeleted => {
      if(Object.keys(isDeleted).length > 0){
        if(isDeleted.executed){
          this._projectService.loadAll("");
          this._toastrService.show("Project deleted!", "Action", { status: "danger", icon: "trash-2-outline" });
        }
      }
    });
    
    this._projectService.loadAll("");

    this.formSearch = this._formBuilder.group({
      query: "",
    });
  }

  onSubmit(search){
    this._projectService.loadAll(search.query);
  }

  onEdit(event): void{
    this._router.navigateByUrl('/admin/editar-projeto/' + event.data.id);
  }

  onDeleteConfirm(register): void {
    this.dialogService.open(DialogDeleteComponent, {
      context: {
        message: `Do you want to delete project ${register.data.name}?`
      },
    })
    .onClose.subscribe(clientConfirmed => this.onDeleteConfirmed(register.data, clientConfirmed));
  }

  onDeleteConfirmed(client, deleteConfirmed){
    if(deleteConfirmed){
      this._projectService.delete(client.id);
    }
  }

  buildForm(project){
    this.form = this._formBuilder.group({
      name: [project.name, Validators.required],
      code: [project.code, Validators.required],
      startDate: [project.startDate, Validators.required],
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
    .onClose.subscribe(project => this._projectService.loadAll(""));
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

}
