using Human_Resource_API.Models;
using MediatR;

namespace Human_Resource_API.Queries
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public int EmployeeId { get; }

        public GetEmployeeQuery(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
