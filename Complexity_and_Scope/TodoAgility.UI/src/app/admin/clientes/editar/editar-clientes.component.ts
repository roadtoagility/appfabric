import { Component, OnDestroy, OnInit } from '@angular/core';
import {ClientService} from '../../services/client.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'ngx-editar-clientes',
  templateUrl: './editar-clientes.component.html',
  styleUrls: ['./editar-clientes.component.scss']
})
export class EditarClientesComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any>;

  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
