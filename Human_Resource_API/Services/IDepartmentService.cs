using Human_Resource_API.Models;

namespace Human_Resource_API.Services
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentById(int id);
        Department CreateDepartment(Department department);
        Department UpdateDepartment(int id, Department department);
        Department DeleteDepartment(int id);
    }
}
