using Human_Resource_API.Models;
using MediatR;
using System.Collections.Generic;

namespace Human_Resource_API.Queries
{
    public class GetEmployeesQuery : IRequest<IEnumerable<Employee>>
    {
    }
}