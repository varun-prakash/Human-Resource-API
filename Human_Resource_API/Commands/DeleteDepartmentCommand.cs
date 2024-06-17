using MediatR;

namespace Human_Resource_API.Commands
{
    public class DeleteDepartmentCommand : IRequest
    {
        public int DepartmentId { get; }

        public DeleteDepartmentCommand(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
