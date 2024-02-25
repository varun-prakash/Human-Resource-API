using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Human_Resource_API.Services; // Make sure to include the namespace for the service
using Microsoft.AspNetCore.Mvc;

namespace Human_Resource_API.Controllers
{
    [Route($"api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                _logger.LogInformation("Making Get All Departments call");
                var departments = _departmentService.GetAllDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetDepartments failed with the error :" + ex.Message);
                throw;
            }
        }

        [HttpGet("{id:int}", Name = "GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Department> GetDepartment(int id)
        {
            try
            {
                _logger.LogInformation($"Making Get Department with id={id} call");
                var department = _departmentService.GetDepartmentById(id);
                if (department == null) return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetDepartment by Id failed with the error :" + ex.Message);
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Department> CreateDepartment([FromBody] Department newDepartment)
        {
            _logger.LogInformation("Making create Department call");

            if (newDepartment == null) return BadRequest();

            var createdDepartment = _departmentService.CreateDepartment(newDepartment);
            return CreatedAtRoute("GetDepartment", new { id = createdDepartment.DepartmentId }, createdDepartment);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Department> UpdateDepartment(int id, [FromBody] Department updatedDepartment)
        {
            _logger.LogInformation($"Making update Department with id={id} call");

            if (updatedDepartment == null || id != updatedDepartment.DepartmentId) return BadRequest();

            var department = _departmentService.UpdateDepartment(id, updatedDepartment);
            if (department == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoveDepartment(int id)
        {
            _logger.LogInformation($"Making remove Department with id={id} call");

            var department = _departmentService.DeleteDepartment(id);
            if (department == null) return NotFound();

            return NoContent();
        }
    }
}
