import { Component, Input, OnDestroy, Injectable } from '@angular/core';
import { NbWindowRef, NbDialogRef } from '@nebular/theme';
import { LocalDataSource } from 'ng2-smart-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ProjectService } from '../../services/project.service';

@Component({
  templateUrl: './new-project-modal.component.html',
  styleUrls: ['new-project-modal.component.scss'],
})
export class NewProjectFormComponent implements OnDestroy {
  
  private _unsubscribeAll: Subject<any>;

  @Input() title: string;

  constructor(private _projectService: ProjectService, protected ref: NbDialogRef<NewProjectFormComponent>) {
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
