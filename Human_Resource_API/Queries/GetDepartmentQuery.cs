using Human_Resource_API.Models;
using MediatR;
namespace Human_Resource_API.Queries
{
    public class GetDepartmentQuery: IRequest<Department>
    {
        public int DepartmentId { get; }

        public GetDepartmentQuery(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
