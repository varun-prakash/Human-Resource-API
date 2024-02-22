using Human_Resource_API.Models;
using Human_Resource_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resource_API.Controllers
{
    [Route("api/HR")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id:int}", Name = "GetEmployee")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee newEmployee)
        {
            var createdEmployee = _employeeService.CreateEmployee(newEmployee);
            return CreatedAtRoute("GetEmployee", new { id = createdEmployee.EmployeeId }, createdEmployee);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            var employee = _employeeService.UpdateEmployee(id, updatedEmployee);
            if (employee == null) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Employee> RemoveEmployee(int id)
        {
            var employee = _employeeService.DeleteEmployee(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }
    }
}
