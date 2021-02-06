import { NgModule } from "@angular/core";
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogDeleteComponent } from './dialog-delete.component';

import {
    NbCardModule,
    NbIconModule,
    NbButtonModule,
  } from '@nebular/theme';

@NgModule({
    imports: [
        NbCardModule,
        NbIconModule,
        NbButtonModule
    ],
    entryComponents: [
        DialogDeleteComponent
    ],
    declarations:[
        DialogDeleteComponent
    ]
})
export class DialogDeleteModule{

}