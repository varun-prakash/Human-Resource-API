using Human_Resource_API.Data;
using Human_Resource_API.Models;

namespace Human_Resource_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;

        public EmployeeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _db.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _db.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public Employee CreateEmployee(Employee newEmployee)
        {
            _db.Employees.Add(newEmployee);
            _db.SaveChanges();
            return newEmployee;
        }

        public Employee UpdateEmployee(int id, Employee updatedEmployee)
        {
            if (updatedEmployee.EmployeeId != id) return null;

            _db.Employees.Update(updatedEmployee);
            _db.SaveChanges();
            return updatedEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
                _db.SaveChanges();
                return employee;
            }
            return null;
        }
    }
}
