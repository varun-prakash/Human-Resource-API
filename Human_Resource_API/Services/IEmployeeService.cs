using Human_Resource_API.Models;

namespace Human_Resource_API.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        Employee CreateEmployee(Employee newEmployee);
        Employee UpdateEmployee(int id, Employee updatedEmployee);
        Employee DeleteEmployee(int id);
    }
}
