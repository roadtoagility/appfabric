import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import {ReleaseService} from '../../services/release.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NewReleaseFormComponent } from '../new/new-release-modal.component';
import { NbDialogService } from '@nebular/theme';

@Component({
  selector: 'releases-list',
  templateUrl: './releases-list.component.html',
  styleUrls: ['./releases-list.component.scss']
})
export class ReleasesListComponent implements OnDestroy {

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
      client: {
        title: 'Cliente',
        type: 'string',
        filter: false
      },
      status: {
        title: 'Status',
        type: 'string',
        filter: false,
      },
      totalEffort: {
        title: 'Esforço total',
        type: 'string',
        filter: false
      }
    },
  };

  source: LocalDataSource = new LocalDataSource();
  entities: any = [];

  constructor(private service: SmartTableData, private _releaseService: ReleaseService, private _router: Router, private dialogService: NbDialogService) {
    const data = this.service.getClients();
    //this.source.load(data);

    this._unsubscribeAll = new Subject();
    
    this._releaseService.onReleasesChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entities = response;
        this.source.load(this.entities);
      }
    });

    this._releaseService.loadAll("");
  }

  onEdit(event): void{
    console.log(event.data);
    this._router.navigateByUrl('/admin/releases-edit/' + event.data.id);
  }

  onDeleteConfirm(event): void {
    console.log(event);
  }

  new(){
    this.dialogService.open(NewReleaseFormComponent, {
      context: {
        title: 'New Release',
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
