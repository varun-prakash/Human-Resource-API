using Human_Resource_API.Models;
using MediatR;

namespace Human_Resource_API.Commands
{
    public class CreateDepartmentCommand : IRequest<Department>
    {
        public Department Department { get; set; }

        public CreateDepartmentCommand(Department department)
        {
            Department = department;
        }
    }
}
