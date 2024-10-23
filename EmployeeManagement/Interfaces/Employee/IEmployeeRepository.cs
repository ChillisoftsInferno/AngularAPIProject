namespace EmployeeManagement.Interfaces.Employee;

public interface IEmployeeRepository
{
    Task<IEnumerable<Models.Employee?>> GetAllEmployeesAsync();
    Task<Models.Employee?> GetEmployeeAsync(int id);
    Task<Models.Employee?> GetEmployeeUsingFiltersAsync(Models.Employee employee);
    Task AddEmployeeAsync(Models.Employee employee);
    Task UpdateEmployeeAsync(Models.Employee employee);
    Task DeleteEmployeeAsync(int id);
}
