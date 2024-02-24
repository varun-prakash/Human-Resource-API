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

    public class EmployeeControllerTests
    {
        private readonly EmployeeController _employeeController;
        private readonly EmployeeService _employeeService;
        private readonly Mock<DbSet<Employee>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<ILogger<EmployeeController>> _mockLogger;
        public EmployeeControllerTests()
        {
            _mockSet = new Mock<DbSet<Employee>>();
            _mockContext = new Mock<ApplicationDbContext>();

           // _mockContext.Setup<DbSet<Employee>>(m => m.Employees).Returns(_mockSet.Object);
            _mockLogger = new Mock<ILogger<EmployeeController>>();

            _employeeService = new EmployeeService(_mockContext.Object);
            _employeeController = new EmployeeController(_employeeService, _mockLogger.Object);

        }

        [Fact]
        public void GetAllEmployees()
        {

            // Arrange
            var data = new List<Employee>
    {
        new Employee { EmployeeId = 1, FirstName = "John Doe" },
        new Employee { EmployeeId = 2, FirstName = "Jane Doe" }
    }.AsQueryable();

            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Act
            var result = _employeeController.GetEmployees();

            // Assert
            //var okResult = result as 
            //var employees = okResult.Value as Employee[];

            //employees.Count().Should().Be(2);

        }
    }
}