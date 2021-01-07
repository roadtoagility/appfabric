import { Component, OnDestroy, OnInit } from '@angular/core';
import {ProjectService} from '../../services/project.service';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import {Location} from '@angular/common';
import { NbDialogService } from '@nebular/theme';
import {ClientSearchFormComponent} from '../../common/modals/client/client-modal.component';

@Component({
  selector: 'ngx-editar-projetos',
  templateUrl: './editar-projetos.component.html',
  styleUrls: ['./editar-projetos.component.scss']
})
export class EditarProjetosComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;
  private id: string;
  private entity: any;
  selectedClient: any;
  //private dialogRef: any;

  constructor(private projectService: ProjectService, private actRoute: ActivatedRoute,  private _location: Location, private dialogService: NbDialogService) { 
    this._unsubscribeAll = new Subject();
    this.entity = {};
    this.selectedClient = { razaoSocial: ""};

    this.projectService.onProjectChanged
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
      this.projectService.load(this.id);
    });
  }

  selectClient(){
    // this.dialogService
    //   .open(ClientSearchFormComponent, { context: { title: `Search for clients`, windowClass:"client-search-modal" }})
    //   .onClose.subscribe(client => console.log(client));
    this.dialogService.open(ClientSearchFormComponent, {
      context: {
        title: 'Search for clients',
      },
    })
    .onClose.subscribe(client => this.selectedClient = client);
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
