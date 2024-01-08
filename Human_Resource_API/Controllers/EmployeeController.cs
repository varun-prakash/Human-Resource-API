using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resource_API.Controllers
{
    [Route($"api/HR")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return Ok(_db.Employees.ToList());
        }


        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Employee>> GetEmployee(int id)
        {
            if (id <= 0) return BadRequest();
            Employee employee = _db.Employees.FirstOrDefault(i => (i.EmployeeId == id));
            if (employee == null) return NotFound();
            return Ok(employee);
        }






    }
}
