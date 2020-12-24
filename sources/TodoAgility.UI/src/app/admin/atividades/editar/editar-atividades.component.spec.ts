import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarAtividadesComponent } from './editar-atividades.component';

describe('EditarAtividadesComponent', () => {
  let component: EditarAtividadesComponent;
  let fixture: ComponentFixture<EditarAtividadesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarAtividadesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarAtividadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
