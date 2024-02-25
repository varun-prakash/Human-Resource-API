using Human_Resource_API.Models;
using Human_Resource_API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Human_Resource_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            // Assuming repository methods are async, you may need to adjust this method to be async as well.
            // For now, we're calling Result to synchronously wait on the task. Consider refactoring to async/await.
            return _employeeRepository.GetAllEmployeesAsync().Result;
        }

        public Employee GetEmployeeById(int id)
        {
            // Same note about async applies here.
            return _employeeRepository.GetEmployeeByIdAsync(id).Result;
        }

        public Employee CreateEmployee(Employee newEmployee)
        {
            // Assuming AddEmployeeAsync is implemented in the repository.
            return _employeeRepository.AddEmployeeAsync(newEmployee).Result;
        }

        public Employee UpdateEmployee(int id, Employee updatedEmployee)
        {
            if (updatedEmployee.EmployeeId != id) return null;

            // Assuming UpdateEmployeeAsync is implemented in the repository.
            _employeeRepository.UpdateEmployeeAsync(id,updatedEmployee).Wait();
            return updatedEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee != null)
            {
                // Assuming DeleteEmployeeAsync is implemented in the repository.
                _employeeRepository.DeleteEmployeeAsync(id).Wait();
                return employee;
            }
            return null;
        }
    }
}
