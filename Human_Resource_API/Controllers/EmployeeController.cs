using Human_Resource_API.Commands;
using Human_Resource_API.Models;
using Human_Resource_API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Human_Resource_API.Controllers
{
    [Route("api/HR")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var query = new GetEmployeesQuery();
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }

        [HttpGet("{id:int}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var query = new GetEmployeeQuery(id);
            var employee = await _mediator.Send(query);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee newEmployee)
        {
            var command = new CreateEmployeeCommand(newEmployee);
            var createdEmployee = await _mediator.Send(command);
            return CreatedAtRoute("GetEmployee", new { id = createdEmployee.EmployeeId }, createdEmployee);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            var command = new UpdateEmployeeCommand(id, updatedEmployee);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            var command = new DeleteEmployeeCommand(id);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
