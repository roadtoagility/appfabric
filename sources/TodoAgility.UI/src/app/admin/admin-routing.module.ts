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
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class AdminRoutingModule {
}
