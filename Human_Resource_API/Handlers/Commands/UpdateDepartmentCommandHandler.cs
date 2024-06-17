using Human_Resource_API.Commands;
using Human_Resource_API.Data;
using MediatR;

namespace Human_Resource_API.Handlers.Commands
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateDepartmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);
            if(department == null)
            {
                throw new KeyNotFoundException("Department Not Found");
            }
            department.DepartmentName = request.Department.DepartmentName;
            _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
