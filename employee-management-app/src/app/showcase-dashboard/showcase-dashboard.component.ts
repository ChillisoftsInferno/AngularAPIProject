import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-showcase-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './showcase-dashboard.component.html',
  styleUrl: './showcase-dashboard.component.css'
})
export class ShowcaseDashboardComponent {
  constructor(private router: Router){}

}
