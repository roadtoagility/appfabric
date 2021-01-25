import { NgModule } from "@angular/core";
import { EditarProjetosComponent } from './edit/projects-edit.component';
import { ListarProjetosComponent } from './list/projects-list.component';
import { NewProjectFormComponent } from './new/new-project-modal.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ProjectService} from '../services/project.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
        NbCardModule,
        Ng2SmartTableModule,
        NbSelectModule,
        NbIconModule,
        NbInputModule,
        NbButtonModule,
        FormsModule,
        ReactiveFormsModule,
        NbSpinnerModule,
        NbDatepickerModule
    ],
    declarations: [
        EditarProjetosComponent,
        ListarProjetosComponent,
        NewProjectFormComponent
    ],
    exports: [
        EditarProjetosComponent,
        ListarProjetosComponent,
        NewProjectFormComponent
    ],
    providers: [ProjectService]
})
export class ProjectsModule{

}