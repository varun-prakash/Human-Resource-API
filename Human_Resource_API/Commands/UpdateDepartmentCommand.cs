using Human_Resource_API.Models;
using MediatR;

namespace Human_Resource_API.Commands
{
    public class UpdateDepartmentCommand : IRequest
    {

        public int DepartmentId { get; }

        public Department Department { get; }

        public UpdateDepartmentCommand(int departmentId, Department department)
        {
            DepartmentId = departmentId;
            Department = department;
        }
    }
}
