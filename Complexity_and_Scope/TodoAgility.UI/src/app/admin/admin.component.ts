import { Component } from '@angular/core';

import { MENU_ITEMS } from './admin-menu';

@Component({
  selector: 'ngx-pages',
  styleUrls: ['admin.component.scss'],
  template: `
    <ngx-one-column-layout>
      <nb-menu [items]="menu"></nb-menu>
      <router-outlet></router-outlet>
    </ngx-one-column-layout>
  `,
})
export class AdminComponent {

  menu = MENU_ITEMS;
}
