import { Component, OnDestroy, OnInit } from '@angular/core';
import { BillingService } from '../../services/billing.service';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Location} from '@angular/common';
import { NbDialogService } from '@nebular/theme';
import { ClientSearchFormComponent} from '../../common/modals/client/client-modal.component';
import { ReleaseSearchFormComponent} from '../../common/modals/release/release-modal.component';


@Component({
  selector: 'billings-edit',
  templateUrl: './billings-edit.component.html',
  styleUrls: ['./billings-edit.component.scss']
})
export class BillingsEditComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;
  private id: string;
  releases: any[];
  private entity: any;

  selectedClient: any;
  selectedReleases: any[];
  //private dialogRef: any;

  constructor(private billingService: BillingService, private actRoute: ActivatedRoute,  private _location: Location, private dialogService: NbDialogService) { 
    this._unsubscribeAll = new Subject();
    this.entity = {};
    this.selectedClient = { name: "", id: 0 };
    this.releases = [];

    this.selectedReleases = [{
      name: "Release-1",
      totalEffort: 200
    },
    {
      name: "Release-2",
      totalEffort: 200
    },
    {
      name: "Release-3",
      totalEffort: 200
    }];

    this.billingService.onBillingChanged
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
      this.billingService.load(this.id);
    });
  }

  removeRelease(releaseToRemove){
    this.releases = this.releases.filter(release => release.id != releaseToRemove.id);
  }

  selectClient(){
    this.dialogService.open(ClientSearchFormComponent, {
      context: {
        title: 'Search for clients',
      },
    })
    .onClose.subscribe(client => this.selectedClient = client);
  }

  selectRelease(){
    this.dialogService.open(ReleaseSearchFormComponent, {
      context: {
        title: 'Search for releases',
        clientId: this.selectedClient.id
      },
    })
    .onClose.subscribe(release => this.releases.push(release));
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
