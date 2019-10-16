using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace HasserisWeb.Controllers
{    
    [Route("employees")]
    public class EmployeeController : Controller
    {
        [Route("index")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index()
        {
            return Content("HEJ");
        }
        
        public ActionResult IndexTest()
        {
            return Content("HEJ2");
        }
        
        [Route("all")]
        public IEnumerable<Employee> GetAllEmployees()        
        {
            return HasserisDbContext.LoadAllElementsFromDatabase("Employee");
        }  
        
        [Route("all/old")]
        public IEnumerable<Employee> GetAllEmployeesOld()        
        {
            Employee[] employees = new Employee[9];

            for (int i = 1; i < 10; i++)
            {
                employees[i - 1] = HasserisDbContext.LoadElementFromDatabase("Employee", 1);
            }

            return employees;
        }  
        
        // Delete
        [Route("delete/{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            HasserisDbContext.DeleteElementFromDatabase<Employee>("Employee", id);
            return Content("Success with: " + id);
        }

        [Route("firstname/{id}")]
        public Employee GetEmployeeFirstName(int id)
        {
            return HasserisDbContext.LoadElementFromDatabase("Employee", id); 
        }
    }
}