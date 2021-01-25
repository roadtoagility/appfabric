import { Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ActivityService } from '../../services/activity.service';

@Component({
  templateUrl: './new-activity-modal.component.html',
  styleUrls: ['new-activity-modal.component.scss'],
})
export class NewActivityFormComponent implements OnDestroy {
  
  private _unsubscribeAll: Subject<any>;

  @Input() title: string;

  constructor(private _activityService: ActivityService, protected ref: NbDialogRef<NewActivityFormComponent>) {
      this._unsubscribeAll = new Subject();
    
  }

  save(){
    this.ref.close("");
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
