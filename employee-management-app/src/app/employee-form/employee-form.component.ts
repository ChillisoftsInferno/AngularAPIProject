import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../models/employee';
import { EmployeeService } from '../employee.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'employee-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css',
})

export class EmployeeFormComponent implements OnInit{

  employee: Employee = {
    id: 0,
    firstName: '',
    lastName: '',
    phone: '',
    email: '',
    position: ''
  };

  errorMessage: string | null = null;

  isEditing: boolean = false
  
  constructor
  (
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) 
  {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((result) => {
      const id = result.get('id');
      if(id) {
        //Edit existing employee
        this.isEditing = true;

        this.employeeService
        .getEmployeeById(Number(id))
        .subscribe
        (
          {
            next: (result) => this.employee = result,
            error: (error) => console.error('Error loading employee', error)
          }
        )
      }
    });
  }

  onSubmit(): void {
    console.log(this.employee);

    if(this.isEditing) {
      this.update();
    } else {
      this.create();
    }
    //Logic to create new employee.
    
  }

  create(): void {
    this.employeeService
    .createEmployee(this.employee)
    .subscribe
    (
      {
        next: () => this.router.navigate(['/']),
        error: (error) => {
          console.error(error);
          this.errorMessage = `Error occured (${error.status})`;
        }
      }
    );
  }

  update(): void {
    this.employeeService
    .updateEmployee(this.employee)
    .subscribe
    (
      {
        next: () => this.router.navigate(['/']),
        error: (error) => {
          console.error(error);
          this.errorMessage = `Error occured (${error.status})`;
        }
      }
    );
  }

}
