import { Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef, NB_THEME_OPTIONS } from '@nebular/theme';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ClientService } from '../../../services/client.service';

import { FormGroup, NgForm } from '@angular/forms';

@Component({
  templateUrl: './dialog-delete.component.html',
  styleUrls: ['dialog-delete.component.scss'],
})
export class DialogDeleteComponent implements OnDestroy {
  
  private _unsubscribeAll: Subject<any>;
  @Input() message: string;


  constructor(protected ref: NbDialogRef<DialogDeleteComponent>) {
    this._unsubscribeAll = new Subject();
 
  }

  confirm() {
    this.ref.close(true);
  }

  cancel() {
    this.ref.close(false);
  }

  ngOnDestroy(): void {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }
}
