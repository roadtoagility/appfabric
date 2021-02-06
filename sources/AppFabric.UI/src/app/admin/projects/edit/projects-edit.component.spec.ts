import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarProjetosComponent } from './projects-edit.component';

describe('EditarProjetosComponent', () => {
  let component: EditarProjetosComponent;
  let fixture: ComponentFixture<EditarProjetosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarProjetosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarProjetosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
