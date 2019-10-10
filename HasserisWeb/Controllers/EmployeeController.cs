using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HasserisWeb.Controllers
{    
    [ApiController]
    [Route("getemployees")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Employee> Get()        
        {
            Employee[] employees = new Employee[9];

            for (int i = 1; i < 10; i++)
            {
                employees[i - 1] = HasserisDbContext.LoadElementFromDatabase("Employee", i);
            }

            return employees;
        }  
    }
}