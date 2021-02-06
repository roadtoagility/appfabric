import { Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { BillingService } from '../../services/billing.service';

@Component({
  templateUrl: './new-billing-modal.component.html',
  styleUrls: ['new-billing-modal.component.scss'],
})
export class NewBillingFormComponent implements OnDestroy {
  
  private _unsubscribeAll: Subject<any>;

  @Input() title: string;

  constructor(private _projectService: BillingService, protected ref: NbDialogRef<NewBillingFormComponent>) {
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
