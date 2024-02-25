namespace Human__Resource_API.test
{
    using Human_Resource_API.Models;
    using Human_Resource_API.Services;
    using Human_Resource_API.Controllers;
    using Human_Resource_API.Data;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Moq.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Human_Resource_API.Repositories;
    using Microsoft.AspNetCore.Mvc;

    public class EmployeeControllerTests
    {
        private readonly EmployeeController _employeeController;
        private readonly EmployeeService _employeeService;
        private readonly Mock<DbSet<Employee>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<ILogger<EmployeeController>> _mockLogger;
        private readonly Mock<IEmployeeRepository> _mockRepo;



        public EmployeeControllerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _mockLogger = new Mock<ILogger<EmployeeController>>();

            var employeeService = new EmployeeService(_mockRepo.Object);
            _employeeController = new EmployeeController(employeeService, _mockLogger.Object);


        }

        [Fact]
        public void GetAllEmployees()
        {

            // Arrange
            _mockRepo.Setup(repo => repo.GetAllEmployeesAsync()).ReturnsAsync(TestingData.GetFakeEmployeeList());

            // Act
            var result = _employeeController.GetEmployees();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Employee>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<Employee>>(okResult.Value);
            Assert.Equal(TestingData.GetFakeEmployeeList().Count, returnValue.Count);
            // Assert.True(true);

        }


        [Fact]
        public void GetEmployeeById()
        {
            // Arrange
            var fakeEmployee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "J.D@gmail.com",
                ContactNumber = "123-456-7890",
                Position = "Software Developer", // Assuming a position
                DepartmentId = 1, // Assuming a department ID
                StartDate = DateTime.Now, // Assuming start date is now
                IsActive = true // Assuming the employee is currently active
            };
            _mockRepo.Setup(repo => repo.GetEmployeeByIdAsync(fakeEmployee.EmployeeId)).ReturnsAsync(fakeEmployee);

            // Act
            var result = _employeeController.GetEmployee(fakeEmployee.EmployeeId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Employee>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(fakeEmployee.EmployeeId, returnValue.EmployeeId);
            Assert.Equal(fakeEmployee.FirstName, returnValue.FirstName);
            Assert.Equal(fakeEmployee.LastName, returnValue.LastName);
        }

        [Fact]
        public void CreateEmployee()
        {
            // Arrange
            var newEmployee = new Employee { FirstName = "New", LastName = "Employee" };
            _mockRepo.Setup(repo => repo.AddEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(newEmployee);

            // Act
            var result =  _employeeController.CreateEmployee(newEmployee);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
            var returnValue = Assert.IsType<Employee>(createdAtActionResult.Value);
            Assert.Equal(newEmployee.FirstName, returnValue.FirstName);
            Assert.Equal(newEmployee.LastName, returnValue.LastName);
        }



        [Fact]
        public void DeleteEmployee()
        {
            // Arrange
            int employeeIdToDelete = 1;
            _mockRepo.Setup(repo => repo.GetEmployeeByIdAsync(employeeIdToDelete)).ReturnsAsync(new Employee { EmployeeId = employeeIdToDelete });
            _mockRepo.Setup(repo => repo.DeleteEmployeeAsync(employeeIdToDelete)).Returns(Task.CompletedTask);

            // Act
            var result =  _employeeController.RemoveEmployee(employeeIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }




    }
}