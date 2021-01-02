import { Component, OnDestroy, OnInit } from '@angular/core';
import {ClientService} from '../../services/client.service';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {States} from '../../common/data/cityState';
import {Location} from '@angular/common';

@Component({
  selector: 'ngx-editar-clientes',
  templateUrl: './editar-clientes.component.html',
  styleUrls: ['./editar-clientes.component.scss']
})
export class EditarClientesComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;
  private id: string;
  private entity: any;
  form: FormGroup;
  statusForm: FormGroup;
  statesData: States;
  states: any[];

  constructor(private clientService: ClientService, private actRoute: ActivatedRoute, private _formBuilder: FormBuilder, private _location: Location) {
    this._unsubscribeAll = new Subject();
    this.entity = {};

    this.statesData = new States();
    this.states = [];

    this.statesData.getEstados().forEach(state => {
      this.states.push({value: state.shortcut, viewValue: state.name});
    });
    
    
    this.statusForm = this._formBuilder.group({
      newStatus: ['']
    });
   }

  backClicked() {
    this._location.back();
  }

  createForm(){
    this.form = this._formBuilder.group({
      razaosocial: [this.entity.razaosocial, Validators.required],
      cnpj: [this.entity.cnpj, Validators.required],
      emailComercial: [this.entity.emailComercial, [Validators.required, Validators.email]],
    });
  }

  ngOnInit(): void {
    this.clientService.onClientChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entity = response;
        console.log(this.entity);
        this.createForm();
      }
    });

    this.actRoute.paramMap.subscribe(params => {
      this.id = params.get('id');
      this.clientService.load(this.id);
    });
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
