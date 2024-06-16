using Human_Resource_API.Data;
using Microsoft.EntityFrameworkCore;
using Human_Resource_API.Extensions;
using Serilog;
using Human_Resource_API.Services;
using Human_Resource_API.Repositories;
using MediatR;
using System.Reflection;
using Human_Resource_API.Handlers.Commands;
using Human_Resource_API.Handlers.Queries;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("log/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true;
    option.RespectBrowserAcceptHeader = true;
});

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

//Add cors configuration
builder.Services.ConfigureCors();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<CreateEmployeeCommandHandler>();
builder.Services.AddScoped<UpdateEmployeeCommandHandler>();
builder.Services.AddScoped<DeleteEmployeeCommandHandler>();
builder.Services.AddScoped<GetEmployeesQueryHandler>();
builder.Services.AddScoped<GetEmployeeQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
