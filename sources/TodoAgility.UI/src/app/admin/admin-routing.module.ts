import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AdminComponent } from './admin.component';
import { ListarProjetosComponent } from './projetos/listar/listar-projetos.component'
import { ListarClientesComponent } from './clientes/listar/listar-clientes.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ListarAtividadesComponent } from './atividades/listar/listar-atividades.component';
import { EditarProjetosComponent } from './projetos/editar/editar-projetos.component';
import { EditarClientesComponent } from './clientes/editar/editar-clientes.component';
import { EditarAtividadesComponent } from './atividades/editar/editar-atividades.component';

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
      path: 'clientes',
      component: ListarClientesComponent,
    },
    {
      path: 'editar-cliente/:id',
      component: EditarClientesComponent,
    },
    {
      path: 'projetos',
      component: ListarProjetosComponent,
    },
    {
      path: 'editar-projeto/:id',
      component: EditarProjetosComponent,
    },
    {
      path: 'atividades',
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
