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

  constructor(private _projectService: ProjectService, protected ref: NbDialogRef<NewProjectFormComponent>) {
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
    console.log(this.form);
  }

  onSubmit(formSubmitted: NgForm, project) {
    console.log("salvando 1...");
    console.log(project);
    console.log(formSubmitted);
    if (this.form.status === 'VALID' && this.form.touched === true) {
      
      this._projectService.save(project);

      formSubmitted.resetForm();
      //this.buildForm(new Project({}));
      this.form.markAsUntouched();
      this.form.markAsPristine();

      //this.titleButtonAction = this.ADICIONAR_LABEL;
      //this.openSnackBar('Dados de taxas do condomínio atualizados com sucesso', 'Fechar');
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
