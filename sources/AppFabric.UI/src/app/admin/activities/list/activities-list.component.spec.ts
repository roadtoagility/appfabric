import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarAtividadesComponent } from './listar-atividades.component';

describe('ListarAtividadesComponent', () => {
  let component: ListarAtividadesComponent;
  let fixture: ComponentFixture<ListarAtividadesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarAtividadesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarAtividadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
