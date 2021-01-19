import { Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ClientService } from '../../services/client.service';

import { FormGroup, NgForm } from '@angular/forms';

@Component({
  templateUrl: './new-client-modal.component.html',
  styleUrls: ['new-client-modal.component.scss'],
})
export class NewClientFormComponent implements OnDestroy {
  
  private _unsubscribeAll: Subject<any>;

  @Input() title: string;
  @Input() form: any;
  statusForm: FormGroup;

  constructor(private _clientService: ClientService, protected ref: NbDialogRef<NewClientFormComponent>) {
      this._unsubscribeAll = new Subject();
    
  }

  onSubmit(formSubmitted: NgForm, project) {

    if (this.form.status === 'VALID' && this.form.touched === true) {
      this._clientService.save(project);

      formSubmitted.resetForm();
      this.form.markAsUntouched();
      this.form.markAsPristine();
    }
    
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
