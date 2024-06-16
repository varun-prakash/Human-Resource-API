using Human_Resource_API.Commands;
using Human_Resource_API.Data;
using MediatR;

namespace Human_Resource_API.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateEmployeeCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.EmployeeId);
            if(employee == null)
            {
                throw new KeyNotFoundException("Employee Not Found");
            }
            employee.FirstName = request.Employee.FirstName;
            employee.LastName = request.Employee.LastName;
            employee.Position = request.Employee.Position;
            employee.DepartmentId = request.Employee.DepartmentId;
            employee.ContactNumber = request.Employee.ContactNumber;
            employee.Email = request.Employee.Email;
            employee.StartDate = request.Employee.StartDate;
            employee.TerminationDate = request.Employee.TerminationDate;
            employee.TerminationReason = request.Employee.TerminationReason;
            employee.IsActive = request.Employee.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
}
