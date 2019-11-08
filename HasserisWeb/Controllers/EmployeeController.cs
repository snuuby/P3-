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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        /* Just testing
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
        */
        
        [Route("all")]
        public string GetAllEmployees()        
        {
            dynamic temp = HasserisDbContext.LoadAllElementsFromDatabase("Employee");
            return JsonConvert.SerializeObject((temp));
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
        
=======
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

        [Route("{id}")]
        public string GetSpecificEmployee(int id)
        {
            return JsonConvert.SerializeObject(database.Employees
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }



>>>>>>> Stashed changes
=======

        [Route("{id}")]
        public string GetSpecificEmployee(int id)
        {
            return JsonConvert.SerializeObject(database.Employees
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }



>>>>>>> Stashed changes
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
        }
    }
}