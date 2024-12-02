import { Routes } from '@angular/router';
import { EmployeeTableComponent } from './employee-table/employee-table.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { ShowcaseDashboardComponent } from './showcase-dashboard/showcase-dashboard.component';
import { CssTransitionsDashboardComponent } from './+css-transitions/css-transitions-dashboard/css-transitions-dashboard.component';

export const routes: Routes = 
[
    {path: '', component: EmployeeTableComponent},
    {path: 'create', component: EmployeeFormComponent},
    {path: 'edit/:id?name', component: EmployeeFormComponent},
    {path: 'employees', redirectTo: '', pathMatch: 'full'},
    {path: 'showcase', component: ShowcaseDashboardComponent},
    {path: 'css-transitions-dashboard', component: CssTransitionsDashboardComponent}
];
