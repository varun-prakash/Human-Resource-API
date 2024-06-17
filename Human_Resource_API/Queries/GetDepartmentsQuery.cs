using Human_Resource_API.Models;
using MediatR;
namespace Human_Resource_API.Queries
{
    public class GetDepartmentsQuery : IRequest<IEnumerable<Department>>
    {

        public GetDepartmentsQuery()
        {
        }
    }
}
