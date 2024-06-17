using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Human_Resource_API.Queries;
using MediatR;

namespace Human_Resource_API.Handlers.Queries
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, Department>
    {
        private readonly ApplicationDbContext _context;

        public GetDepartmentQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var Department = await _context.Departments.FindAsync(request.DepartmentId);
            if (Department == null)
            {
                throw new KeyNotFoundException("Department Not Found");
            }

            return Department;
        }

    }
}
