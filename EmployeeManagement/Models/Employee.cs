using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models;

public class Employee
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Phone Number is required")]
    [Length(10,10)]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "Address is required")]
    public string Position { get; set; }
}
