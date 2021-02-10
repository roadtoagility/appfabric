import { Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ReleaseService } from '../../services/release.service';

@Component({
  templateUrl: './new-release-modal.component.html',
  styleUrls: ['new-release-modal.component.scss'],
})
export class NewReleaseFormComponent implements OnDestroy {
  
  private _unsubscribeAll: Subject<any>;

  @Input() title: string;

  constructor(private _releaseService: ReleaseService, protected ref: NbDialogRef<NewReleaseFormComponent>) {
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
