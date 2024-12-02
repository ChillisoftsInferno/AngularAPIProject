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
        //Validation
        if (ModelState.IsValid == false) return BadRequest();
        
        await _employeeRepository.AddEmployeeAsync(employee);
        return CreatedAtAction(nameof(FindEmployeeById), new { id = employee.Id }, employee);
    } 
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> FindAllEmployees()
    {
        var allEmployees = await _employeeRepository.GetAllEmployeesAsync();
        return !allEmployees.Any() ? NotFound() : Ok(allEmployees);
    } 
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
    {
        //Validation
        if(id != employee.Id) return BadRequest();
        if (ModelState.IsValid == false) return BadRequest();
        
        await _employeeRepository.UpdateEmployeeAsync(employee);
        return CreatedAtAction(nameof(FindEmployeeById), new { id = employee.Id }, employee);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Employee>>> FindEmployeeById(int id)
    {
        var employee = await _employeeRepository.GetEmployeeAsync(id);
        return employee == null ? NotFound(employee) : Ok(employee);
    } 
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployeeById(int id)
    {
        await _employeeRepository.DeleteEmployeeAsync(id);
        return NoContent();
    } 
}
