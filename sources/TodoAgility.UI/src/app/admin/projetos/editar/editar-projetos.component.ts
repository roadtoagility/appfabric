import { Component, OnDestroy, OnInit } from '@angular/core';
import {ProjectService} from '../../services/project.service';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import {Location} from '@angular/common';
import { NbDialogService } from '@nebular/theme';
import {ClientSearchFormComponent} from '../../common/modals/client/client-modal.component';
import {Project} from '../../models/entities/Project';
import { FormBuilder, FormGroup, Validators, NgForm } from '@angular/forms';

@Component({
  selector: 'ngx-editar-projetos',
  templateUrl: './editar-projetos.component.html',
  styleUrls: ['./editar-projetos.component.scss']
})
export class EditarProjetosComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;
  selectedClient: any;

  loading = false;
  errors: any[];
  success = false;
  private id: string;
  private entity: any;

  form: FormGroup;
  formSubmitted: any;

  formDetails: FormGroup;
  formDetailsSubmitted: any;

  constructor(private _formBuilder: FormBuilder, private projectService: ProjectService, private actRoute: ActivatedRoute,  private _location: Location, private dialogService: NbDialogService) { 
    this._unsubscribeAll = new Subject();
    this.entity = {};
    this.selectedClient = { name: ""};

   

    this.projectService.onProjectChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.errors = [];
        this.success = true;
        this.toggleLoadingAnimation();
        setTimeout(() => this.success = false, 2000);
      }
    });
    
    this.projectService.onProjectUpdateError
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(errors => {
      if(Object.keys(errors).length > 0){
        this.errors = errors;
        this.toggleLoadingAnimation();
      }
    });
  }

  ngOnInit(): void {
    this.projectService.onProjectChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entity = response;
        this.buildForm(new Project(this.entity));
      }
    });

    this.actRoute.paramMap.subscribe(params => {
      this.id = params.get('id');
      this.projectService.load(this.id);
      this.buildForm(new Project({id: this.id}));
    });
  }

  toggleLoadingAnimation() {
    this.loading = !this.loading;
  }

  buildForm(project){
    this.form = this._formBuilder.group({
      id: [project.id, Validators.required],
      name: [project.name, Validators.required],
      code: [project.code, Validators.required],
      startDate: [project.startDate, [Validators.required]],
      isFavorited: project.isFavorited 
    });

    this.formDetails = this._formBuilder.group({
      budget: [project.budget, [Validators.pattern(/[1-4]/g)]],
      status: project.status,
      hourCost: [project.hourCost, [Validators.pattern(/[1-4]/g)]],
      profitMargin: [project.profitMargin, [Validators.pattern(/[1-4]/g)]],
      clientId: project.clientId,
      manager: project.manager,
      orderNumber: project.orderNumber,
    });
  }

  onSubmit(formSubmitted: NgForm, projectData, projectDetails) {
    this.toggleLoadingAnimation();
    this.formSubmitted = formSubmitted;
    
    if (this.form.status === 'VALID' && this.formDetails.status === 'VALID') {
      let project = new Project(projectData);
      project.update(projectDetails);
      this.projectService.update(project);
    }else{
      this.errors = [{message: "Check required fields."}];
      this.toggleLoadingAnimation();
    }
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
    .onClose.subscribe(client => {
      this.selectedClient = client;
      this.formDetails.patchValue({clientId: client.id});
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
