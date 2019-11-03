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
        
        [Route("all")]
        public string GetAllEmployees()        
        {
            using (var db = new HasserisDbContext())
            {
                return JsonConvert.SerializeObject(db.Employees.ToList());
            }
        }  
        

        
        // Delete
        [Route("delete/{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            using (var db = new HasserisDbContext())
            {
                var employee = db.Employees.FirstOrDefault(e => e.ID == id);
                employee.Employed = "Unemployed";
                db.Employees.Update(employee);
                db.SaveChanges();
            }
            return Content("Success with: " + id);
        }

        [Route("firstname/{id}")]
        public Employee GetEmployeeFirstName(int id)
        {
            using (var db = new HasserisDbContext())
            {
                return db.Employees.FirstOrDefault(e => e.ID == id);
                
            }
        }
    }
}