import { Component, Input, OnDestroy } from '@angular/core';
import { NbSortDirection, NbSortRequest, NbTreeGridDataSource, NbTreeGridDataSourceBuilder } from '@nebular/theme';
import {DashboardService} from '../services/dashboard.service';
import { Subject } from 'rxjs';

interface TreeNode<T> {
  data: T;
  children?: TreeNode<T>[];
  expanded?: boolean;
}

interface FSEntry {
  name: string;
  items?: number;
}

@Component({
  selector: 'ngx-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnDestroy{

  customColumn = 'name';
  defaultColumns = [  ]
  // defaultColumns = [ 'size', 'kind', 'items' ];
  // allColumns = [ this.customColumn, ...this.defaultColumns ];

  allColumns = [ this.customColumn]

  dataSource: NbTreeGridDataSource<FSEntry>;

  sortColumn: string;
  sortDirection: NbSortDirection = NbSortDirection.NONE;
  private _unsubscribeAll: Subject<any>;

  constructor(private dataSourceBuilder: NbTreeGridDataSourceBuilder<FSEntry>, private dashboardService: DashboardService) {
    this.dataSource = this.dataSourceBuilder.create(this.data);
  }

  updateSort(sortRequest: NbSortRequest): void {
    this.sortColumn = sortRequest.column;
    this.sortDirection = sortRequest.direction;
  }

  getSortDirection(column: string): NbSortDirection {
    if (this.sortColumn === column) {
      return this.sortDirection;
    }
    return NbSortDirection.NONE;
  }

  private data: TreeNode<FSEntry>[] = [
    {
      data: { name: 'Portal do Cliente' },
      children: [
        { data: { name: 'project-1.doc' } },
        { data: { name: 'project-2.doc'} },
        { data: { name: 'project-3'} },
        { data: { name: 'project-4.docx' } },
      ],
    },
    {
      data: { name: 'Sistema de Controle de Qualidade', items: 2 },
      children: [
        { data: { name: 'Report 1' } },
        { data: { name: 'Report 2' } },
      ],
    },
    {
      data: { name: 'Compliance', items: 2 },
      children: [
        { data: { name: 'backup.bkp' } },
        { data: { name: 'secret-note.txt' } },
      ],
    },
  ];

  getShowOn(index: number) {
    const minWithForMultipleColumns = 400;
    const nextColumnStep = 100;
    return minWithForMultipleColumns + (nextColumnStep * index);
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

}

@Component({
  selector: 'ngx-fs-icon',
  template: `
    <nb-tree-grid-row-toggle [expanded]="expanded" *ngIf="isDir(); else fileIcon">
    </nb-tree-grid-row-toggle>
    <ng-template #fileIcon>
      <nb-icon icon="cube-outline"></nb-icon>
    </ng-template>
  `,
})
export class FsIconComponent {
  @Input() kind: string;
  @Input() expanded: boolean;

  isDir(): boolean {
    return this.kind === 'dir';
  }
}




