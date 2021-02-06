import { Component, OnDestroy, OnInit } from '@angular/core';
import {ActivityService} from '../../services/activity.service';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import {Location} from '@angular/common';

@Component({
  selector: 'ngx-editar-atividades',
  templateUrl: './activities-edit.component.html',
  styleUrls: ['./activities-edit.component.scss']
})
export class EditarAtividadesComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;
  private id: string;
  private entity: any;

  constructor(private activityService: ActivityService, private actRoute: ActivatedRoute, private _location: Location) { 
    this._unsubscribeAll = new Subject();
    this.entity = {};
    
    this.activityService.onActivitiyChanged
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
      this.activityService.load(this.id);
    });
  }

  backClicked() {
    this._location.back();
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
}

}
