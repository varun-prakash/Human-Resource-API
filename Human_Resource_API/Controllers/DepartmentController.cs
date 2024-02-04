using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resource_API.Controllers
{
    [Route($"api/Department")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ApplicationDbContext db, ILogger<DepartmentController> logger)
        {
            _logger = logger;
            _db = db;
        }

       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                _logger.LogInformation("Making Get All Department call");
                var departments = _db.Departments.ToList();
                return Ok(departments);
            }
            catch(Exception ex)
            {
                _logger.LogError("GetDepartments failed with the error :" + ex.Message);
                throw;
            }
        }


        [HttpGet("{id:int}", Name = "GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Department>> GetDepartment(int id)
        {
            try {
                _logger.LogInformation("Making Get Department with id call");
                if (id <= 0) return BadRequest();
                Department department = _db.Departments.FirstOrDefault(i => (i.DepartmentId == id));
                if (department == null) return NotFound();
                return Ok(department);
            }
            catch(Exception ex)
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

            var depId = _db.Departments.FirstOrDefault<Department>(i => (i.DepartmentId == newDepartment.DepartmentId));

            if (depId == null)
            {
                _db.Departments.Add(newDepartment);
                _db.SaveChanges();
                return CreatedAtRoute("GetDepartment", new { id = newDepartment.DepartmentId });
            }
            return BadRequest();

        }

        [HttpPut("{id:int}")]
       
        public ActionResult<Department> UpdateDepartment(int id, [FromBody]Department updatedDepartment)
        {
            _logger.LogInformation("Making update Department call");

            if (updatedDepartment == null) return BadRequest();

            var getId = _db.Departments.FirstOrDefault(i => i.DepartmentId == id);
            if (getId == null) return BadRequest();

            if (updatedDepartment.DepartmentId != id) return BadRequest();
            
            _db.Departments.Update(updatedDepartment);
            _db.SaveChanges();

            return NoContent();
        }


        [HttpDelete]

        public ActionResult<Department> RemoveDepartment(int id)
        {
            if(id==0) return BadRequest();
            var depId = _db.Departments.FirstOrDefault(i=> (i.DepartmentId == id));
            if (depId == null) return BadRequest();

            _db.Remove(depId);
            _db.SaveChanges();

            return Ok(depId);
        }


    }
}
