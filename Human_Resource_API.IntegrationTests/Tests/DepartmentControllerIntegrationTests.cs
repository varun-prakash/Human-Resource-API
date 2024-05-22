using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Human_Resource_API.Models;
using System.Net.Http.Json;

public class DepartmentControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DepartmentControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllDepartments_ReturnsOkResponse()
    {
        // Act
        var response = await _client.GetAsync("/api/Department");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var departments = JsonConvert.DeserializeObject<List<Department>>(responseString);
        Assert.IsType<List<Department>>(departments);
    }

    [Fact]
    public async Task GetDepartmentById_ReturnsOkResponse()
    {
        // Arrange
        var department = new Department { DepartmentId = 1, DepartmentName = "HR" };
        await _client.PostAsJsonAsync("/api/Department", department);

        // Act
        var response = await _client.GetAsync($"/api/Department/{department.DepartmentId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var fetchedDepartment = JsonConvert.DeserializeObject<Department>(responseString);
        Assert.Equal(department.DepartmentId, fetchedDepartment.DepartmentId);
        Assert.Equal(department.DepartmentName, fetchedDepartment.DepartmentName);
    }

    [Fact]
    public async Task CreateDepartment_ReturnsCreatedResponse()
    {
        // Arrange
        var newDepartment = new Department { DepartmentName = "HR" };
        var content = new StringContent(JsonConvert.SerializeObject(newDepartment), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/Department", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var createdDepartment = JsonConvert.DeserializeObject<Department>(responseString);
        Assert.Equal(newDepartment.DepartmentName, createdDepartment.DepartmentName);
    }

    [Fact]
    public async Task DeleteDepartment_ReturnsNoContentResponse()
    {
        // Arrange
        var department = new Department { DepartmentId = 2, DepartmentName = "Finance" };
        await _client.PostAsJsonAsync("/api/Department", department);

        // Act
        var response = await _client.DeleteAsync($"/api/Department/{department.DepartmentId}");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }
}
