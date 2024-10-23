using EmployeeManagement.Interfaces.Employee;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

// http://localhost:5052/api/employee
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase , IEmployeeController
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
    }
    
    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        await _employeeRepository.AddEmployeeAsync(employee);
        return Created();
    } 
    
    [HttpGet]
    public async Task<ActionResult<Employee>> FindAllEmployees()
    {
        await _employeeRepository.GetAllEmployeesAsync();
        return Ok();
    } 
    
    [HttpPut]
    public async Task<ActionResult<Employee>> UpdateEmployees(Employee employee)
    {
        await _employeeRepository.UpdateEmployeeAsync(employee);
        return Ok();
    } 
}
