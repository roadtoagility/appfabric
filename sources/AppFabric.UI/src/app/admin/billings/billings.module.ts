import { NgModule } from "@angular/core";
import { Ng2SmartTableModule } from 'ng2-smart-table';

import { BillingsEditComponent } from './edit/billings-edit.component';
import { BillingsListComponent } from './list/billings-list.component';
import { NewBillingFormComponent } from './new/new-billing-modal.component';

import { BillingService} from '../services/billing.service';

import {
    NbButtonModule,
    NbCardModule,
    NbIconModule,
    NbInputModule,
    NbSelectModule,
    NbAccordionModule
  } from '@nebular/theme';

@NgModule({
    imports: [
        Ng2SmartTableModule,
        NbCardModule,
        NbInputModule,
        NbSelectModule,
        NbIconModule,
        NbAccordionModule,
        NbButtonModule
    ],
    declarations: [
        BillingsEditComponent,
        BillingsListComponent,
        NewBillingFormComponent
    ],
    exports: [
        BillingsEditComponent,
        BillingsListComponent,
        NewBillingFormComponent
    ],
    providers: [BillingService]
})
export class BillingsModule{

}