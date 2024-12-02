import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CssTransitionsDashboardComponent } from './css-transitions-dashboard.component';

describe('CssTransitionsDashboardComponent', () => {
  let component: CssTransitionsDashboardComponent;
  let fixture: ComponentFixture<CssTransitionsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CssTransitionsDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CssTransitionsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
