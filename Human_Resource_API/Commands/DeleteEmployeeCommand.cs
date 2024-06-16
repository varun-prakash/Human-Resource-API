using MediatR;

namespace Human_Resource_API.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {

        public int EmployeeId { get; }

        public DeleteEmployeeCommand(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
