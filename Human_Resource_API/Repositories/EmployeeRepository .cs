using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Human_Resource_API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateEmployeeAsync(int id, Employee employeeToUpdate)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found with ID " + id);
            }

            // Update properties
            employee.FirstName = employeeToUpdate.FirstName;
            employee.LastName = employeeToUpdate.LastName;
            employee.Email = employeeToUpdate.Email;
            employee.StartDate = employeeToUpdate.StartDate;
            employee.ContactNumber = employeeToUpdate.ContactNumber;
            employee.Position = employeeToUpdate.Position;
            employee.DepartmentId = employeeToUpdate.DepartmentId;
            employee.TerminationDate = employeeToUpdate.TerminationDate;
            employee.TerminationReason = employeeToUpdate.TerminationReason;
            employee.IsActive = employeeToUpdate.IsActive;


            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
