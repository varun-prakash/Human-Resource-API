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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult<Employee> CreateEmployee([FromBody]Employee newEmployee)
        {
            if(newEmployee == null) return BadRequest();

            var empId = _db.Employees.FirstOrDefault<Employee>(i => (i.EmployeeId == newEmployee.EmployeeId) );

            if(empId == null)
            {
                _db.Employees.Add(newEmployee);
                _db.SaveChanges();
                return CreatedAtRoute("GetEmployee", new { id = newEmployee.EmployeeId });
            }
            return BadRequest();

        }


        [HttpDelete]

        public ActionResult<Employee> RemoveEmployee(int id)
        {
            if(id==0) return BadRequest();
            var empId = _db.Employees.FirstOrDefault(i=> (i.EmployeeId == id));
            if (empId == null) return BadRequest();

            _db.Remove(empId);
            _db.SaveChanges();

            return Ok(empId);
        }


    }
}
