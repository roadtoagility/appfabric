import { Component, OnDestroy, OnInit } from '@angular/core';
import {ActivityService} from '../../services/activity.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'ngx-editar-atividades',
  templateUrl: './editar-atividades.component.html',
  styleUrls: ['./editar-atividades.component.scss']
})
export class EditarAtividadesComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;

  constructor(private activityService: ActivityService) { 
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
}

}
