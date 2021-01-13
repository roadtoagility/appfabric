import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { ActivityModalService } from './activity-modal.service';
import { LocalDataSource, ViewCell } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  templateUrl: './activity-modal.component.html',
  styleUrls: ['activity-modal.component.scss'],
})
export class ActivitySearchFormComponent implements OnDestroy {
  
    activityNameSearch: string;

    activities: any[];
    private _unsubscribeAll: Subject<any>;

    @Input() title: string;
    @Input() projectId: number;

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
          titulo: {
            title: 'Title',
            type: 'string',
            editable: false,
            filter: false
          }
        },
      };

    source: LocalDataSource = new LocalDataSource();
    entities: any = [];

    constructor(private _activityService: ActivityModalService, protected ref: NbDialogRef<ActivitySearchFormComponent>) {
        this._unsubscribeAll = new Subject();
        this.activityNameSearch = "";

        this._activityService.onActivitiesChanged
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(response => {
        if(Object.keys(response).length > 0){
            this.entities = response;
            this.source.load(this.entities);
        }
        }); 
    }

    searchActivity(){
        this._activityService.search(this.activityNameSearch, this.projectId);
    }

    onSelectActivity(event): void{
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
