using Human_Resource_API.Models;
using Human_Resource_API.Services;
using Human_Resource_API.Controllers;
using Human_Resource_API.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using Human_Resource_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Human__Resource_API.test;

namespace Human_Resource_API.test
{
    public class DepartmentControllerTests
    {
        private readonly DepartmentController _departmentController;
        private readonly DepartmentService _departmentService;
        private readonly Mock<IDepartmentRepository> _mockRepo;
        private readonly Mock<ILogger<DepartmentController>> _mockLogger;

        public DepartmentControllerTests()
        {
            _mockRepo = new Mock<IDepartmentRepository>();
            _mockLogger = new Mock<ILogger<DepartmentController>>();

            _departmentService = new DepartmentService(_mockRepo.Object);
            _departmentController = new DepartmentController(_departmentService, _mockLogger.Object);
        }

        [Fact]
        public void GetAllDepartments()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllDepartmentsAsync()).ReturnsAsync(TestingData.GetFakeDepartments());

            // Act
            var result = _departmentController.GetDepartments();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Department>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<Department>>(okResult.Value);
            Assert.Equal(TestingData.GetFakeDepartments().Count, returnValue.Count);
        }

        [Fact]
        public void GetDepartmentById()
        {
            // Arrange
            var fakeDepartment = new Department
            {
                DepartmentId = 1,
                DepartmentName = "Engineering"
            };
            _mockRepo.Setup(repo => repo.GetDepartmentbyIdAsync(fakeDepartment.DepartmentId)).ReturnsAsync(fakeDepartment);

            // Act
            var result = _departmentController.GetDepartment(fakeDepartment.DepartmentId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Department>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Department>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(fakeDepartment.DepartmentId, returnValue.DepartmentId);
            Assert.Equal(fakeDepartment.DepartmentName, returnValue.DepartmentName);
        }

        [Fact]
        public void CreateDepartment()
        {
            // Arrange
            var newDepartment = new Department { DepartmentName = "New Department" };
            _mockRepo.Setup(repo => repo.AddDepartmentAsync(It.IsAny<Department>())).ReturnsAsync(newDepartment);

            // Act
            var result = _departmentController.CreateDepartment(newDepartment);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Department>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
            var returnValue = Assert.IsType<Department>(createdAtActionResult.Value);
            Assert.Equal(newDepartment.DepartmentName, returnValue.DepartmentName);
        }

        [Fact]
        public void DeleteDepartment()
        {
            // Arrange
            int departmentIdToDelete = 1;
            _mockRepo.Setup(repo => repo.GetDepartmentbyIdAsync(departmentIdToDelete)).ReturnsAsync(new Department { DepartmentId = departmentIdToDelete });
            _mockRepo.Setup(repo => repo.DeleteDepartmentAsync(departmentIdToDelete)).Returns(Task.CompletedTask);

            // Act
            var result = _departmentController.RemoveDepartment(departmentIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
