import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import {ReleaseModalService} from './release-modal.service';
import { LocalDataSource, ViewCell } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  templateUrl: './release-modal.component.html',
  styleUrls: ['release-modal.component.scss'],
})
export class ReleaseSearchFormComponent implements OnDestroy {
  
    releaseNameSearch: string;

    releases: any[];
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
          name: {
            title: 'Title',
            type: 'custom',
            filter: false,
            renderComponent: TitleComponent,
          }
        },
      };

    source: LocalDataSource = new LocalDataSource();
  entities: any = [];

    constructor(private _releaseService: ReleaseModalService, protected ref: NbDialogRef<ReleaseSearchFormComponent>) {
        this._unsubscribeAll = new Subject();
        this.releaseNameSearch = "";

        this._releaseService.onReleasesChanged
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(response => {
        if(Object.keys(response).length > 0){
            this.entities = response;
            this.source.load(this.entities);
        }
        }); 
    }

    searchRelease(){
        this._releaseService.search(this.releaseNameSearch, this.clientId);
    }

    onSelectRelease(event): void{
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

@Component({
  template: `
    {{renderValue}}
  `,
})
export class TitleComponent implements ViewCell, OnInit {

  renderValue: string;

  @Input() value: string | number;
  @Input() rowData: any;

  ngOnInit() {
    this.renderValue = `Release-${this.rowData.id}`;
    this.value = this.rowData.id;
  }

}
