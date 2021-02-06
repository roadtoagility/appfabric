import { NgModule } from "@angular/core";
import { EditarClientesComponent } from './edit/clients-edit.component';
import { ListarClientesComponent } from './list/clients-list.component';
import { NewClientFormComponent } from './new/new-client-modal.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ClientService} from '../services/client.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {DisplayErrorComponent} from '../common/components/display-error/display-error.component';
import { DisplayErrorModule } from '../common/components/display-error/display-error.module';
import { DialogDeleteModule } from '../common/modals/delete/dialog-delete.module';
import { CommonModule } from '@angular/common';  

import {
    NbButtonModule,
    NbCardModule,
    NbDatepickerModule,
    NbIconModule,
    NbInputModule,
    NbSelectModule,
    NbSpinnerModule
  } from '@nebular/theme';

@NgModule({
    imports: [
        CommonModule,
        NbCardModule,
        Ng2SmartTableModule,
        NbSelectModule,
        NbIconModule,
        NbInputModule,
        NbButtonModule,
        FormsModule,
        ReactiveFormsModule,
        NbSpinnerModule,
        NbDatepickerModule,
        DialogDeleteModule
    ],
    declarations: [
        EditarClientesComponent,
        ListarClientesComponent,
        NewClientFormComponent
    ],
    exports: [
        EditarClientesComponent,
        ListarClientesComponent,
        NewClientFormComponent
    ],
    providers: [ClientService]
})
export class ClientsModule{

}