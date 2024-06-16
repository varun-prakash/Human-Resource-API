using Human_Resource_API.Commands;
using Human_Resource_API.Data;
using MediatR;

namespace Human_Resource_API.Handlers.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }   

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.EmployeeId);
            if(employee == null)
            {
                throw new KeyNotFoundException("Employee Does Not Exist");
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }

    }
}
