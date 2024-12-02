import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-css-transitions-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './css-transitions-dashboard.component.html',
  styleUrl: './css-transitions-dashboard.component.css'
})
export class CssTransitionsDashboardComponent {

  public constructor(private router: Router){}
}
