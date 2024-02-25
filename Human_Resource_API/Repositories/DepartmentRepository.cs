using Human_Resource_API.Data;
using Human_Resource_API.Migrations;
using Human_Resource_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentbyIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(e => e.DepartmentId == id);
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
             _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }


        public async Task UpdateDepartmentAsync(int id, Department department)
        {
            var oldDepartment = await _context.Departments.FirstOrDefaultAsync(e => e.DepartmentId == id);

            if(oldDepartment == null)
            {
                throw new KeyNotFoundException("Department not found with ID " + id);
            }

            oldDepartment.DepartmentName = department.DepartmentName;


            _context.Departments.Update(oldDepartment);

            await _context.SaveChangesAsync();

        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }




    }
}
