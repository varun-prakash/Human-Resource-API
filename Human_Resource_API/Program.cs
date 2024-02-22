using Human_Resource_API.Data;
using Microsoft.EntityFrameworkCore;
using Human_Resource_API.Extensions;
using Serilog;
using Human_Resource_API.Services;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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
