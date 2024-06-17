using Human_Resource_API.Commands;
using Human_Resource_API.Data;
using Human_Resource_API.Models;
using MediatR;

namespace Human_Resource_API.Handlers.Commands
{
    public class CreateDepartmentCommandHandler: IRequestHandler<CreateDepartmentCommand, Department>
    {
        private readonly ApplicationDbContext _context;

        public CreateDepartmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _context.Departments.Add(request.Department);
             await _context.SaveChangesAsync(cancellationToken);

            return request.Department;
        }
    }
}
