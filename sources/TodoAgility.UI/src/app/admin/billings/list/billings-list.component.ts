import { Component, Input, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import { BillingService } from '../../services/billing.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'billings-list',
  templateUrl: './billings-list.component.html',
  styleUrls: ['./billings-list.component.scss']
})
export class BillingsListComponent implements OnDestroy {

  private _unsubscribeAll: Subject<any>;

  settings = {
    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>'
    },
    actions: {
      edit: true,
      add: false,
      columnTitle: 'Ações'
    },
    mode:'external',
    columns: {
      id: {
        title: 'ID',
        type: 'number',
        editable: false,
        filter: false
      },
      client: {
        title: 'Client',
        type: 'string',
        filter: false
      },
      amout: {
        title: 'Amout',
        type: 'number',
        filter: false,
      },
      status: {
        title: 'Status',
        type: 'string',
        filter: false
      }
    },
  };

  source: LocalDataSource = new LocalDataSource();
  entities: any = [];

  constructor(private service: SmartTableData, private _billingService: BillingService, private _router: Router) {
    const data = this.service.getClients();
    //this.source.load(data);

    this._unsubscribeAll = new Subject();
    
    this._billingService.onBillingsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.entities = response;
        this.source.load(this.entities);
      }
    });

    this._billingService.loadAll("");
  }

  onEdit(event): void{
    this._router.navigateByUrl('/admin/edit-billing/' + event.data.id);
  }

  onDeleteConfirm(event): void {
    console.log(event);
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
