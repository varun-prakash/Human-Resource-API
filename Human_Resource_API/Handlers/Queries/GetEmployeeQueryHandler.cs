using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Human_Resource_API.Queries;
using MediatR;

namespace Human_Resource_API.Handlers.Queries
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Employee>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeeQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee Not Found");
            }

            return employee;
        }

    }
}
