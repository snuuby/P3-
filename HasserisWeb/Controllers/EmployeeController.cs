using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace HasserisWeb.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
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

                return database.Employees.FirstOrDefault(e => e.ID == id);
                
            
        }
    }
}