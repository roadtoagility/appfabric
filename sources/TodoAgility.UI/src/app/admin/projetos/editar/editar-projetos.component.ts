import { Component, OnDestroy, OnInit } from '@angular/core';
import {ProjectService} from '../../services/project.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'ngx-editar-projetos',
  templateUrl: './editar-projetos.component.html',
  styleUrls: ['./editar-projetos.component.scss']
})
export class EditarProjetosComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;

  constructor(private projectService: ProjectService) { 
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
