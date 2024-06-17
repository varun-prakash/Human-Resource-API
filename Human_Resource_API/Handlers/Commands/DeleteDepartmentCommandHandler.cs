using Human_Resource_API.Commands;
using Human_Resource_API.Data;
using MediatR;

namespace Human_Resource_API.Handlers.Commands
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteDepartmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);

            if(department == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
