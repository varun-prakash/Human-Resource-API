using Human_Resource_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Department> Departments { get; set; }

    }
}
