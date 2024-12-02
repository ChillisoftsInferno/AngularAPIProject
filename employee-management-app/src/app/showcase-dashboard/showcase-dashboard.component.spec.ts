import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowcaseDashboardComponent } from './showcase-dashboard.component';

describe('ShowcaseDashboardComponent', () => {
  let component: ShowcaseDashboardComponent;
  let fixture: ComponentFixture<ShowcaseDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShowcaseDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowcaseDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
