using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HasserisWeb.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        // Dependency injection by constructor
        public HasserisDbContext database;
        
        public EmployeeController(HasserisDbContext sc)
        {
            database = sc;
        }

        // Method to retrieve all employees for the employee overview in the view component.
        [Route("all")]
        public string GetAllEmployees()
        {
            return JsonConvert.SerializeObject(database.Employees.Include(address => address.Address).
                                                Include(contact => contact.ContactInfo).ToList());
        }
        [Route("available")]
        public string GetAvailableEmployees()
        {
            return JsonConvert.SerializeObject(database.Employees.
                Where(employee => employee.IsAvailable && (employee.Type == "Admin" || employee.Type == "AdminPlus")).
                Include(contact => contact.ContactInfo).Include(address => address.Address).ToList());
        }
        
        // Method to retrieve a specific employee by ID
        [Route("{id}")]
        public string GetSpecificEmployee(int id)
        {
            return JsonConvert.SerializeObject(database.Employees
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }

        /* Dead code? should be deleted
        // Delete
        [Route("delete/{id}")]
        public ActionResult DeleteEmployee(int id)
        {

            var employee = database.Employees.FirstOrDefault(e => e.ID == id);
            employee.Employed = "Unemployed";
            database.Employees.Update(employee);
            database.SaveChanges();

            return Content("Success with: " + id);
        }

        [Route("firstname/{id}")]
        public Employee GetEmployeeFirstName(int id)
        {
            return HasserisDbContext.LoadElementFromDatabase("Employee", id);
        }*/
    }
}
