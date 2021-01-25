import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AdminComponent } from './admin.component';
import { ListarProjetosComponent } from './projects/list/projects-list.component'
import { ListarClientesComponent } from './clients/list/clients-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ListarAtividadesComponent } from './activities/list/activities-list.component';
import { EditarProjetosComponent } from './projects/edit/projects-edit.component';
import { EditarClientesComponent } from './clients/edit/clients-edit.component';
import { EditarAtividadesComponent } from './activities/edit/activities-edit.component';

import { ReleasesListComponent } from './releases/list/releases-list.component';
import { ReleasesEditComponent } from './releases/edit/releases-edit.component';
import { BillingsListComponent } from './billings/list/billings-list.component';
import { BillingsEditComponent } from './billings/edit/billings-edit.component';

const routes: Routes = [{
  path: '',
  component: AdminComponent,
  children: [
    {
      path: 'dashboard',
      component: DashboardComponent,
    },
    {
      path: 'clients',
      component: ListarClientesComponent,
    },
    {
      path: 'editar-cliente/:id',
      component: EditarClientesComponent,
    },
    {
      path: 'projects',
      component: ListarProjetosComponent,
    },
    {
      path: 'editar-projeto/:id',
      component: EditarProjetosComponent,
    },
    {
      path: 'activities',
      component: ListarAtividadesComponent,
    },
    {
      path: 'editar-atividade/:id',
      component: EditarAtividadesComponent,
    },
    {
      path: 'releases',
      component: ReleasesListComponent,
    },
    {
      path: 'releases-edit/:id',
      component: ReleasesEditComponent,
    },
    {
      path: 'billings',
      component: BillingsListComponent,
    },
    {
      path: 'billings-edit/:id',
      component: BillingsEditComponent,
    },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class AdminRoutingModule {
}
