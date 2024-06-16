using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Human_Resource_API.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_API.Handlers.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }
    }
}
