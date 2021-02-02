import { Component, OnDestroy, OnInit } from '@angular/core';
import {ClientService} from '../../services/client.service';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';
import {States} from '../../common/data/cityState';
import {Location} from '@angular/common';
import { ClientData } from 'app/admin/models/entities/ClientData';

@Component({
  selector: 'ngx-editar-clientes',
  templateUrl: './clients-edit.component.html',
  styleUrls: ['./clients-edit.component.scss']
})
export class EditarClientesComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;
  private id: string;
  private entity: any;

  form: FormGroup;
  formSubmitted: any;

  formDetails: FormGroup;
  formDetailsSubmitted: any;

  statesData: States;
  states: any[];
  loading = false;
  errors: any[];
  success = false;

  constructor(private clientService: ClientService, private actRoute: ActivatedRoute, private _formBuilder: FormBuilder, private _location: Location) {
    this._unsubscribeAll = new Subject();
    this.entity = {};

    this.statesData = new States();
    this.states = [];
    this.errors = [];

    this.statesData.getEstados().forEach(state => {
      this.states.push({value: state.shortcut, viewValue: state.name});
    });

    this.clientService.onClientChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.errors = [];
        this.success = true;
        this.toggleLoadingAnimation();
        setTimeout(() => this.success = false, 2000);
      }
    });
    
    this.clientService.onClientLoaded
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(client => {
      if(Object.keys(client).length > 0){
        this.buildForm(client);
      }
    });

    this.clientService.onClientUpdateError
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(errors => {
      if(Object.keys(errors).length > 0){
        this.errors = errors;
        this.toggleLoadingAnimation();
      }
    });
   }

  backClicked() {
    this._location.back();
  }

  buildForm(client){
    this.form = this._formBuilder.group({
      id: [client.id, Validators.required],
      name: [client.name, Validators.required],
      cnpj: [client.cnpj, Validators.required],
      commercialEmail: [client.commercialEmail, [Validators.required, Validators.email]]
    });

    this.formDetails = this._formBuilder.group({
      address: client.address,
      state: client.state,
      city: client.city,
      cep: client.cep,
      businessArea: client.businessArea,
      businessManager: client.businessManager,
      commercialPhone: client.commercialPhone,
    });
  }

  ngOnInit(): void {
    this.actRoute.paramMap.subscribe(params => {
      this.id = params.get('id');
      this.clientService.load(this.id);
      this.buildForm(new ClientData({id: this.id}));
    });
  }

  toggleLoadingAnimation() {
    this.loading = !this.loading;
  }

  onSubmit(formSubmitted: NgForm, clientData, clientDetails) {
    this.toggleLoadingAnimation();
    this.formSubmitted = formSubmitted;
    
    if (this.form.status === 'VALID' && this.formDetails.status === 'VALID') {
      let client = new ClientData(clientData);
      client.update(clientDetails);
      this.clientService.save(client);
    }else{
      this.errors = [{message: "Check required fields."}];
      this.toggleLoadingAnimation();
    }
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
