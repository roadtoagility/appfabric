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
  loading = false;

  @Input() title: string;
  @Input() form: any;
  statusForm: FormGroup;

  constructor(private _clientService: ClientService, protected ref: NbDialogRef<NewClientFormComponent>) {
    this._unsubscribeAll = new Subject();

    this._clientService.onClientChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        
        this.form.markAsUntouched();
        this.form.markAsPristine();
      }
    });
  }

  toggleLoadingAnimation() {
    this.loading = true;
    setTimeout(() => this.loading = false, 1000);
  }

  onSubmit(formSubmitted: NgForm, client) {
    this.toggleLoadingAnimation();

    if (this.form.status === 'VALID' && this.form.touched === true) {
      this._clientService.save(client);

      //formSubmitted.resetForm();
      this.form.markAsUntouched();
      this.form.markAsPristine();
      this.ref.close(client);
    }

    //this.toggleLoadingAnimation();
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
