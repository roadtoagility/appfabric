import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarClientesComponent } from './clients-edit.component';

describe('EditarClientesComponent', () => {
  let component: EditarClientesComponent;
  let fixture: ComponentFixture<EditarClientesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarClientesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
