import { Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef, NB_THEME_OPTIONS } from '@nebular/theme';
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
  private _formSubmitted: any;

  loading = false;
  success = false;

  @Input() title: string;
  @Input() form: any;
  statusForm: FormGroup;

  errors: any[];

  savedObject: any;

  constructor(private _clientService: ClientService, protected ref: NbDialogRef<NewClientFormComponent>) {
    this._unsubscribeAll = new Subject();

    this.errors = [];

    this._clientService.onClientChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this._formSubmitted.resetForm();
        this.form.markAsUntouched();
        this.form.markAsPristine();
        

        this.showCompleteForm();
        this.errors = [];
        this.success = true;
        this.toggleLoadingAnimation();
        this.savedObject = response;
        console.log(response);
      }
    });

    this._clientService.onClientUpdateError
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(errors => {
      if(Object.keys(errors).length > 0){
        this.errors = errors;
        this.toggleLoadingAnimation();
      }
    });
  }

  showCompleteForm(){

  }

  toggleLoadingAnimation() {
    this.loading = !this.loading;
  }

  onSubmit(formSubmitted: NgForm, client) {
    this.toggleLoadingAnimation();
    this._formSubmitted = formSubmitted;
    
    if (this.form.status === 'VALID' && this.form.touched === true) {
      this._clientService.save(client);
    }else{
      this.errors = [{message: "Check required fields."}];
      this.toggleLoadingAnimation();
    }
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
