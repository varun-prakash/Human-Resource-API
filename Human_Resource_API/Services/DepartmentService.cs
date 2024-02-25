using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Human_Resource_API.Repositories;

namespace Human_Resource_API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAllDepartmentsAsync().Result;
        }

        public Department GetDepartmentById(int id)
        {
            return _departmentRepository.GetDepartmentbyIdAsync(id).Result;
        }

        public Department CreateDepartment(Department department)
        {
            return _departmentRepository.AddDepartmentAsync(department).Result;
            
        }

        public Department UpdateDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return null; 
            }

            _departmentRepository.UpdateDepartmentAsync(id, department).Wait();
            return department;
        }

        public Department DeleteDepartment(int id)
        {
            var department = GetDepartmentById(id);
            if (department != null)
            {
                _departmentRepository.DeleteDepartmentAsync(id).Wait();
                return department;
            }
            return null;
        }
    }
}
