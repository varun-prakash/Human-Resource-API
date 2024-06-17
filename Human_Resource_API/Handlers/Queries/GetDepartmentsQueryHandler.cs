using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Human_Resource_API.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_API.Handlers.Queries
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, IEnumerable<Department>>
    {
        private readonly ApplicationDbContext _context;

        public GetDepartmentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Departments.ToListAsync(cancellationToken);
        }
    }
}
