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
    [Route("customers")]
    public class CustomerController : Controller
    {
        public HasserisDbContext database;
        public CustomerController(HasserisDbContext sc)
        {
            database = sc;
        }
        [Route("all")]
        public string GetAllCustomers()
        {
                return JsonConvert.SerializeObject(database.Customers.ToList());
            
        }  
        
        /*
        // Delete
        [Route("delete/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            HasserisDbContext.DeleteElementFromDatabase(Customer,id);
            return Content("Success with: " + id);
        }
        
        [Route("firstname/{id}")]
        public Employee GetEmployeeFirstName(int id)
        {
            return HasserisDbContext.LoadElementFromDatabase("Employee", id); 
        }
        */
    }
}