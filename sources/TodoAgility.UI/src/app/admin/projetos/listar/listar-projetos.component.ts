import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ProjectService} from '../../services/project.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';

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

  constructor(private service: SmartTableData, private _projetosService: ProjectService, private _router: Router) {
    const data = this.service.getProjects();
    //this.source.load(data);
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

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

}
