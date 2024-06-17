using Human_Resource_API.Data;
using Microsoft.EntityFrameworkCore;
using Human_Resource_API.Extensions;
using Serilog;
using MediatR;
using System.Reflection;
using Human_Resource_API.Handlers.Commands;
using Human_Resource_API.Handlers.Queries;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("log/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

// Add Controllers
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.RespectBrowserAcceptHeader = true;
});

// Add Authentication and Identity
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

// Add CORS configuration
builder.Services.ConfigureCors();

// Add MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


// Register Handlers
builder.Services.AddScoped<CreateEmployeeCommandHandler>();
builder.Services.AddScoped<UpdateEmployeeCommandHandler>();
builder.Services.AddScoped<DeleteEmployeeCommandHandler>();
builder.Services.AddScoped<GetEmployeesQueryHandler>();
builder.Services.AddScoped<GetEmployeeQueryHandler>();
builder.Services.AddScoped<CreateDepartmentCommandHandler>();
builder.Services.AddScoped<UpdateDepartmentCommandHandler>();
builder.Services.AddScoped<DeleteDepartmentCommandHandler>();
builder.Services.AddScoped<GetDepartmentsQueryHandler>();
builder.Services.AddScoped<GetDepartmentQueryHandler>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
