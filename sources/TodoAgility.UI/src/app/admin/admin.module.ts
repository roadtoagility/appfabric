import { NgModule } from '@angular/core';
import { NbAccordionModule, NbMenuModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { AdminComponent } from './admin.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { AdminRoutingModule } from './admin-routing.module';
import { ProjectsModule } from './projects/projects.module';
import { ClientsModule } from './clients/clients.module';
import { ActivitiesModule } from './activities/activities.module';

import { ClientSearchFormComponent} from './common/modals/client/client-modal.component';
import { ProjectSearchFormComponent} from './common/modals/project/project-modal.component';
import { ReleaseSearchFormComponent} from './common/modals/release/release-modal.component';
import { ActivitySearchFormComponent} from './common/modals/activity/activity-modal.component';

import { ReleasesModule } from './releases/releases.module';
import { BillingsModule } from './billings/billings.module';

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

import { Ng2SmartTableModule } from 'ng2-smart-table';
import { FsIconComponent } from './activities/list/activities-list.component';

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
    DashboardModule,
    NbInputModule,
    Ng2SmartTableModule,
    NbListModule,
    NbAccordionModule,
    ...materialModules,
    ActivitiesModule,
    BillingsModule,
    ClientsModule,
    ProjectsModule,
    ReleasesModule
  ],
  declarations: [
    AdminComponent,
    FsIconComponent,
    ClientSearchFormComponent,
    ReleaseSearchFormComponent,
    ActivitySearchFormComponent,
    ProjectSearchFormComponent
  ],
  providers:[FormsModule, ProjectModalService, ClientService, ProjectService, DashboardService, ActivityService, ClientModalService, ReleaseService, BillingService, ReleaseModalService, ActivityModalService]
})
export class AdminModule {
}
