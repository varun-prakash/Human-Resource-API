using Human_Resource_API.Models;
using MediatR;

namespace Human_Resource_API.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get;}

        public UpdateEmployeeCommand(int employeeId, Employee employee)
        {
            EmployeeId = employeeId;
            Employee = employee;
        }
    }
}
