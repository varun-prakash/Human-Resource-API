using Human_Resource_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human__Resource_API.test
{
    internal class TestingData
    {

        public static List<Employee> GetFakeEmployeeList()
        {
            return new List<Employee>()
    {
       new Employee
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
},
new Employee
{
    EmployeeId = 2,
    FirstName = "Mark",
    LastName = "Luther",
    Email = "M.L@gmail.com",
    ContactNumber = "123-456-7890",
    Position = "Project Manager", // Assuming a position
    DepartmentId = 2, // Assuming a different department ID
    StartDate = DateTime.Now, // Assuming start date is now
    IsActive = true // Assuming the employee is currently active
}

    };
        }


        public static List<Department> GetFakeDepartments()
        {
            return new List<Department>()
            {
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "Enngineering"
                },

                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "Accounts"
                }
            };

        }

    }
}
