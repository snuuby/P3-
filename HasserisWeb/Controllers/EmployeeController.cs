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
        
        public HasserisDbContext database;
        public EmployeeController(HasserisDbContext sc)
        {
            database = sc;
        }
        [Route("all")]
        public string GetAllEmployees()        
        {

                return JsonConvert.SerializeObject(database.Employees.ToList());
            
        }
        [Route("{id}")]
        public string GetSpecificEmployee(int id)
        {
            return JsonConvert.SerializeObject(database.Employees
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }

        /*
        // Delete
        [Route("delete/{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            HasserisDbContext.EmployeeNoLongerEmployee(id);
            return Content("Success with: " + id);
        }

        [Route("firstname/{id}")]
        public Employee GetEmployeeFirstName(int id)
        {
            return HasserisDbContext.LoadElementFromDatabase("Employee", id); 
        }*/
    }
}