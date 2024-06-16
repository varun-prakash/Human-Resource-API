using Human_Resource_API.Models;
using MediatR;

namespace Human_Resource_API.Commands
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public Employee Employee { get; }

        public CreateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
    }
}
