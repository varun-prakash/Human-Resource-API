using Human_Resource_API.Models;

namespace Human_Resource_API.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentbyIdAsync(int id);
        Task<Department> AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(int id, Department department);
        Task DeleteDepartmentAsync(int departmentId);
    }
}
