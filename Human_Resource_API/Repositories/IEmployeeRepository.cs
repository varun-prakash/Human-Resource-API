using Human_Resource_API.Models;

namespace Human_Resource_API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int id,Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
