﻿using Human_Resource_API.Commands;
using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Human_Resource_API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resource_API.Controllers
{
    [Route($"api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator, ILogger<DepartmentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            try
            {
                _logger.LogInformation("Making Get All Departments call");
                var query = new GetDepartmentsQuery();
                var departments = await _mediator.Send(query);
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
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            try
            {
                _logger.LogInformation($"Making Get Department with id={id} call");
                var query = new GetDepartmentQuery(id);
                var department = await _mediator.Send<Department>(query);
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
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department newDepartment)
        {
            _logger.LogInformation("Making create Department call");

            if (newDepartment == null) return BadRequest();
            var command = new CreateDepartmentCommand(newDepartment);
            var createdDepartment = await _mediator.Send(command);
            return CreatedAtRoute("GetDepartment", new { id = createdDepartment.DepartmentId }, createdDepartment);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, [FromBody] Department updatedDepartment)
        {
            _logger.LogInformation($"Making update Department with id={id} call");

            if (updatedDepartment == null || id != updatedDepartment.DepartmentId) return BadRequest();

            var command = new UpdateDepartmentCommand(id, updatedDepartment);
            var department = await _mediator.Send(command);
            if (department == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            _logger.LogInformation($"Making remove Department with id={id} call");

            var command = new DeleteDepartmentCommand(id);
            var department = await _mediator.Send(command);
            if (department == null) return NotFound();

            return NoContent();
        }
    }
}
