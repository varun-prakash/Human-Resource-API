using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Human_Resource_API;
using Human_Resource_API.Data;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add DbContext using an in-memory database for testing
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(serviceProvider);
            });

            var sp = services.BuildServiceProvider();

            // Ensure the database is created
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
        });
    }
}
