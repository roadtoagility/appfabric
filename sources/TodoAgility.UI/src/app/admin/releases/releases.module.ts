import { NgModule } from "@angular/core";
import { ReleasesEditComponent } from './edit/releases-edit.component';
import { ReleasesListComponent } from './list/releases-list.component';
import { NewReleaseFormComponent } from './new/new-release-modal.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ReleaseService} from '../services/release.service';

import {
    NbButtonModule,
    NbCardModule,
    NbIconModule,
    NbInputModule,
    NbSelectModule,
    NbListModule
  } from '@nebular/theme';

@NgModule({
    imports: [
        NbCardModule,
        Ng2SmartTableModule,
        NbSelectModule,
        NbIconModule,
        NbInputModule,
        NbButtonModule,
        NbListModule
    ],
    declarations: [
        ReleasesEditComponent,
        ReleasesListComponent,
        NewReleaseFormComponent
    ],
    exports: [
        ReleasesEditComponent,
        ReleasesListComponent,
        NewReleaseFormComponent
    ],
    providers: [ReleaseService]
})
export class ReleasesModule{

}