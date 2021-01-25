import { NgModule } from "@angular/core";
import { EditarAtividadesComponent } from './edit/activities-edit.component';
import { ListarAtividadesComponent } from './list/activities-list.component';
import { NewActivityFormComponent } from './new/new-activity-modal.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ActivityService} from '../services/activity.service';

import {
    NbButtonModule,
    NbCardModule, 
    NbIconModule,
    NbInputModule,
    NbSelectModule
  } from '@nebular/theme';

@NgModule({
    imports: [
        NbCardModule,
        Ng2SmartTableModule,
        NbSelectModule,
        NbIconModule,
        NbInputModule,
        NbButtonModule
    ],
    declarations: [
        EditarAtividadesComponent,
        ListarAtividadesComponent,
        NewActivityFormComponent
    ],
    exports: [
        EditarAtividadesComponent,
        ListarAtividadesComponent,
        NewActivityFormComponent
    ],
    providers: [ActivityService]
})
export class ActivitiesModule{

}