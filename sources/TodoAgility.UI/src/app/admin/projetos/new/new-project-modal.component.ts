import { OnInit, Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ProjectService } from '../../services/project.service';
import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';
import {Project} from '../../models/entities/Project';

@Component({
  templateUrl: './new-project-modal.component.html',
  styleUrls: ['new-project-modal.component.scss'],
})
export class NewProjectFormComponent implements OnInit, OnDestroy {
  
  private _unsubscribeAll: Subject<any>;

  @Input() title: string;
  @Input() form: any;
  statusForm: FormGroup;
  loading = false;
  success = false;
  errors: any[];
  private _formSubmitted: any;

  constructor(private _projectService: ProjectService, protected ref: NbDialogRef<NewProjectFormComponent>) {
    this._unsubscribeAll = new Subject();
    this.errors = [];

    this._projectService.onProjectChanged
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
      }
    });

    this._projectService.onProjectUpdateError
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

  ngOnInit(): void {
    
  }

  toggleLoadingAnimation() {
    this.loading = true;
    setTimeout(() => this.loading = false, 1000);
  }

  onSubmit(formSubmitted: NgForm, project) {
    this.toggleLoadingAnimation();
    this._formSubmitted = formSubmitted;
    
    if (this.form.status === 'VALID' && this.form.touched === true) {
      project.clientId = +project.clientId;
      this._projectService.save(project);
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
