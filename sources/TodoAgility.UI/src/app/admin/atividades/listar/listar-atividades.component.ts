import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ActivityService} from '../../services/activity.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'ngx-listar-atividades',
  templateUrl: './listar-atividades.component.html',
  styleUrls: ['./listar-atividades.component.scss']
})
export class ListarAtividadesComponent implements OnDestroy {
  
  private _unsubscribeAll: Subject<any>;

  settings = {
    // add: {
    //   addButtonContent: '<i class="nb-plus"></i>',
    //   createButtonContent: '<i class="nb-checkmark"></i>',
    //   cancelButtonContent: '<i class="nb-close"></i>',
    // },
    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
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
      titulo: {
        title: 'Título',
        type: 'string',
        filter: false,
      },
      projeto: {
        title: 'Projeto',
        type: 'string',
        filter: false
      },
      responsavel: {
        title: 'Responsável',
        type: 'string',
        filter: false
      },
      esforco: {
        title: 'Esforço',
        type: 'number',
        filter: false
      },
      status: {
        title: 'Status',
        type: 'html',
        editor: {
          type: 'list',
          config:{
            list: [{title: 'Iniciado', value: 'Iniciado'}, {title: 'Não Iniciado', value: 'Não Iniciado'}, {title: 'Concluído', value: 'Concluído'}]
          }
        },
        filter: false
      }
    },
  };

  source: LocalDataSource = new LocalDataSource();
  entities: any = [];

  constructor(private service: SmartTableData, private _activityService: ActivityService) {
    const data = this.service.getActivities();
    //this.source.load(data);

    this._unsubscribeAll = new Subject();
    
    this._activityService.onActivitiesChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entities = response;
        this.source.load(this.entities);
      }
    });

    this._activityService.loadAll("");
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

@Component({
  selector: 'ngx-fs-icon',
  template: `
    <nb-tree-grid-row-toggle [expanded]="expanded" *ngIf="isDir(); else fileIcon">
    </nb-tree-grid-row-toggle>
    <ng-template #fileIcon>
      <nb-icon icon="file-text-outline"></nb-icon>
    </ng-template>
  `,
})
export class FsIconComponent {
  @Input() kind: string;
  @Input() expanded: boolean;

  isDir(): boolean {
    return this.kind === 'dir';
  }
}
