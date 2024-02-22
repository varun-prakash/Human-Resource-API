using Human_Resource_API.Data;
using Human_Resource_API.Models;
    

namespace Human_Resource_API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
        }

        public Department CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public Department UpdateDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return null; // Or throw an exception based on your error handling policy
            }

            _context.Departments.Update(department);
            _context.SaveChanges();
            return department;
        }

        public Department DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return department;
            }
            return null;
        }
    }
}
