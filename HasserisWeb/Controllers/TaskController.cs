using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace HasserisWeb
{
    [Route("Task")]
    public class TaskController : Controller
    {
        HasserisDbContext database;
        public TaskController(HasserisDbContext database) 
        {
            this.database = database;
        }
        
        [Route("MakeInspectionReport")]
        public void MakeInspectionReport(string json)
        {
            var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
            InspectionReport inspectionReport = JsonConvert.DeserializeObject<InspectionReport>(json, settings);
            Customer tempCustomer_one = new Private("InspectionCustomer", "Eriksen", new Address("Aalborghusvej", "9110", "Aalborg", "Anden dør"), new ContactInfo("lars@gmail.com", "23131313"));
            List<DateTime> testList_two = new List<DateTime>() { new DateTime(2019, 11, 03), new DateTime(2019, 11, 04) };

            Moving tempMoving = new Moving("InspectionTest", tempCustomer_one, new Address("Kukux vej", "9000", "Aalborg", "første dør til venstre"), 700, testList_two, "Hjælp Lars med at flytte", "23131343", tempCustomer_one.Address, 5, true, 1);
            tempMoving.InspectionReport = inspectionReport;
            database.Tasks.Add(tempMoving);
        }
        
    }
}
