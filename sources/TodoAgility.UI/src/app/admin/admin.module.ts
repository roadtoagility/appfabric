import { NgModule } from '@angular/core';
import { NbAccordionModule, NbMenuModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { AdminComponent } from './admin.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { AdminRoutingModule } from './admin-routing.module';
import { ListarProjetosComponent } from './projetos/listar/listar-projetos.component';
import { EditarProjetosComponent } from './projetos/editar/editar-projetos.component';

import { NewProjectFormComponent } from './projetos/new/new-project-modal.component';
import { NewActivityFormComponent } from './atividades/new/new-activity-modal.component';
import { NewBillingFormComponent } from './billings/new/new-billing-modal.component';
import { NewClientFormComponent } from './clientes/new/new-client-modal.component';
import { NewReleaseFormComponent } from './releases/new/new-release-modal.component';

import { ListarClientesComponent } from './clientes/listar/listar-clientes.component';
import { EditarClientesComponent } from './clientes/editar/editar-clientes.component';
import { ListarAtividadesComponent } from './atividades/listar/listar-atividades.component';
import { EditarAtividadesComponent } from './atividades/editar/editar-atividades.component';
import { ClientSearchFormComponent} from './common/modals/client/client-modal.component';
import { ProjectSearchFormComponent} from './common/modals/project/project-modal.component';
import { ReleaseSearchFormComponent} from './common/modals/release/release-modal.component';
import { ActivitySearchFormComponent} from './common/modals/activity/activity-modal.component';
import { ReleasesListComponent } from './releases/list/releases-list.component';
import { ReleasesEditComponent } from './releases/edit/releases-edit.component';
import { BillingsListComponent } from './billings/list/billings-list.component';
import { BillingsEditComponent } from './billings/edit/billings-edit.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
//import { FormsModule as ngFormsModule } from '@angular/forms';

import { Ng2SmartTableModule } from 'ng2-smart-table';
import { FsIconComponent } from './atividades/listar/listar-atividades.component';

import { NbEvaIconsModule } from '@nebular/eva-icons';

import { ClientService} from './services/client.service';
import { ProjectService} from './services/project.service';
import { ActivityService} from './services/activity.service';
import { DashboardService} from './services/dashboard.service';
import { ClientModalService } from './common/modals/client/client-modal.service';
import { ReleaseModalService } from './common/modals/release/release-modal.service';
import { ProjectModalService } from './common/modals/project/project-modal.service';
import { ActivityModalService } from './common/modals/activity/activity-modal.service';
import {ReleaseService} from './services/release.service';
import {BillingService} from './services/billing.service';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const materialModules = [
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatNativeDateModule,
  MatDatepickerModule,
  MatCheckboxModule,
  MatSlideToggleModule,
  MatRadioModule,
  MatButtonModule,
  MatButtonToggleModule,
];

import {
  NbActionsModule,
  NbButtonModule,
  NbCardModule,
  NbCheckboxModule,
  NbDatepickerModule, NbIconModule,
  NbInputModule,
  NbRadioModule,
  NbSelectModule,
  NbUserModule,
  NbTreeGridModule,
  NbListModule,
  NbSpinnerModule
} from '@nebular/theme';


@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    AdminRoutingModule,
    ThemeModule,
    NbTreeGridModule,
    NbMenuModule,
    NbCardModule,
    NbButtonModule,
    NbActionsModule,
    NbUserModule,
    NbCheckboxModule,
    NbRadioModule,
    NbDatepickerModule,
    NbSelectModule,
    NbIconModule,
    NbSpinnerModule,
    NbEvaIconsModule,
    //ngFormsModule,
    DashboardModule,
    NbInputModule,
    Ng2SmartTableModule,
    NbListModule,
    NbAccordionModule,
    //NbDialogRef,
    ...materialModules
  ],
  declarations: [
    AdminComponent,
    ListarProjetosComponent,
    EditarProjetosComponent,
    ListarClientesComponent,
    EditarClientesComponent,
    ListarAtividadesComponent,
    EditarAtividadesComponent,
    FsIconComponent,
    ClientSearchFormComponent,
    ReleaseSearchFormComponent,
    ReleasesListComponent,
    BillingsListComponent,
    ReleasesEditComponent,
    BillingsEditComponent,
    ActivitySearchFormComponent,
    ProjectSearchFormComponent,
    NewProjectFormComponent,
    NewActivityFormComponent,
    NewBillingFormComponent,
    NewClientFormComponent,
    NewReleaseFormComponent
  ],
  providers:[FormsModule, ProjectModalService, ClientService, ProjectService, DashboardService, ActivityService, ClientModalService, ReleaseService, BillingService, ReleaseModalService, ActivityModalService]
})
export class AdminModule {
}
