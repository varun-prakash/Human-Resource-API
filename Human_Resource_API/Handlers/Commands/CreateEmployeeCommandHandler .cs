using Human_Resource_API.Commands;
using Human_Resource_API.Data;
using Human_Resource_API.Models;

namespace Human_Resource_API.Handlers.Commands
{
    public class CreateEmployeeCommandHandler
    {
        private readonly ApplicationDbContext _context;

        public CreateEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _context.Employees.Add(request.Employee);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Employee;
        }


    }
}
