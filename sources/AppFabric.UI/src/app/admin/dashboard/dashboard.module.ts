import { NgModule } from '@angular/core';
import {
  NbActionsModule,
  NbButtonModule,
  NbCardModule,
  NbTabsetModule,
  NbUserModule,
  NbRadioModule,
  NbSelectModule,
  NbListModule,
  NbIconModule, NbInputModule, NbTreeGridModule
} from '@nebular/theme';
import { NgxEchartsModule } from 'ngx-echarts';

import { ThemeModule } from '../../@theme/theme.module';
import { DashboardComponent } from './dashboard.component';
import { FormsModule } from '@angular/forms';
import { FsIconComponent } from './dashboard.component';
import { EchartsBarComponent } from './charts/bar/echarts-bar.component';
import { EchartsPieComponent } from './charts/pie/echarts-pie.component';
import { EchartsAreaStackComponent } from './charts/area/echarts-area-stack.component';


@NgModule({
  imports: [
    FormsModule,
    ThemeModule,
    NbCardModule,
    NbUserModule,
    NbButtonModule,
    NbTabsetModule,
    NbActionsModule,
    NbRadioModule,
    NbSelectModule,
    NbListModule,
    NbIconModule,
    NbButtonModule,
    NgxEchartsModule,
    NbInputModule,
    NbTreeGridModule
  ],
  declarations: [
    DashboardComponent,
    FsIconComponent,
    EchartsBarComponent,
    EchartsPieComponent,
    EchartsAreaStackComponent
  ],
})
export class DashboardModule { }
