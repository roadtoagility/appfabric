import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoadProjectComponent } from './road-project.component';

describe('RoadProjectComponent', () => {
  let component: RoadProjectComponent;
  let fixture: ComponentFixture<RoadProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoadProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoadProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
