import { Component, Input, OnDestroy } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { ProjectModalService } from './project-modal.service';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  templateUrl: './project-modal.component.html',
  styleUrls: ['project-modal.component.scss'],
})
export class ProjectSearchFormComponent implements OnDestroy {
  
    projectNameSearch: string;
    projects: any[];
    private _unsubscribeAll: Subject<any>;

    @Input() title: string;
    @Input() clientId: number;

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
          nome: {
            title: 'Name',
            type: 'string',
            filter: false,
          }
        },
      };

    source: LocalDataSource = new LocalDataSource();
    entities: any = [];

    constructor(private _projectService: ProjectModalService, protected ref: NbDialogRef<ProjectSearchFormComponent>) {
        this._unsubscribeAll = new Subject();
        this.projectNameSearch = "";

        this._projectService.onProjectsChanged
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(response => {
        if(Object.keys(response).length > 0){
            this.entities = response;
            this.source.load(this.entities);
        }
        }); 
    }

    searchProject(){
        this._projectService.search(this.projectNameSearch, this.clientId);
    }

    onSelectProject(event): void{
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
