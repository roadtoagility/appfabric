import { Component, OnDestroy, OnInit } from '@angular/core';
import { ReleaseService} from '../../services/release.service';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import {Location} from '@angular/common';
import { NbDialogService } from '@nebular/theme';
import { ClientSearchFormComponent } from '../../common/modals/client/client-modal.component';
import { ProjectSearchFormComponent } from '../../common/modals/project/project-modal.component';
import { ActivitySearchFormComponent } from '../../common/modals/activity/activity-modal.component';

@Component({
  selector: 'releases-edit',
  templateUrl: './releases-edit.component.html',
  styleUrls: ['./releases-edit.component.scss']
})
export class ReleasesEditComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;
  private id: string;
  private entity: any;
  selectedProject: any;
  selectedClient: any;
  activities: any[];

  selectedActivities: any[];
  //private dialogRef: any;

  constructor(private releaseService: ReleaseService, private actRoute: ActivatedRoute,  private _location: Location, private dialogService: NbDialogService) { 
    this._unsubscribeAll = new Subject();
    this.entity = {};
    
    this.selectedProject = { nome: ""};
    this.selectedClient = { razaoSocial: ""};
    this.activities = [];

    this.selectedActivities = [{
      title: "Activity 1",
      effort: 8
    }];

    this.releaseService.onReleaseChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entity = response;
      }
    });
  }

  ngOnInit(): void {
    this.actRoute.paramMap.subscribe(params => {
      this.id = params.get('id');
      this.releaseService.load(this.id);
    });
  }

  selectClient(){
    this.dialogService.open(ClientSearchFormComponent, {
      context: {
        title: 'Search for clients',
      },
    })
    .onClose.subscribe(client => this.selectedClient = client);
  }

  selectProject(){
    this.dialogService.open(ProjectSearchFormComponent, {
      context: {
        title: 'Search for projects',
        clientId: this.selectedClient.id
      },
    })
    .onClose.subscribe(project => this.selectedProject = project);
  }

  backClicked() {
    this._location.back();
  }

  selectActivity(){
    this.dialogService.open(ActivitySearchFormComponent, {
      context: {
        title: 'Search for activities',
        projectId: this.selectedProject.id
      },
    })
    .onClose.subscribe(activity => this.activities.push(activity));
  }

  removeActivity(activityToRemove){
    this.activities = this.activities.filter(activity => activity.id != activityToRemove.id);
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

}
