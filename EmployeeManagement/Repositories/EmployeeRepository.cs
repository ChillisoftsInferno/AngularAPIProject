using EmployeeManagement.Data;
using EmployeeManagement.Interfaces.Employee;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    
    //Constructor
    public EmployeeRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    //Logic
    public async Task<IEnumerable<Employee?>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(employee => employee.Id == id);
    }

    public async Task<Employee?> GetEmployeeUsingFiltersAsync(Employee employee)
    {
        return await _context.Employees
            .Where(employeeToSearch => employee.FirstName == employeeToSearch.FirstName)
            .Where(employeeToSearch => employee.LastName == employeeToSearch.LastName)
            .Where(employeeToSearch => employee.Email == employeeToSearch.Email)
            .Where(employeeToSearch => employee.Phone == employeeToSearch.Phone)
            .Where(employeeToSearch => employee.Position == employeeToSearch.Position)
            .FirstOrDefaultAsync();

    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);  
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await GetEmployeeAsync(id) ?? throw new KeyNotFoundException($"Unable to delete employee with id [{id}] because it was not found.");
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}
