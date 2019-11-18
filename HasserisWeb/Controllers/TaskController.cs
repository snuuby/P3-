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
            inspectionReport inspectionReport = JsonConvert.DeserializeObject<InspectionReport>(json, settings);

            Moving moving = new Moving();
            moving.inspectionReport = inspectionReport;
            database.Tasks.add(moving);
        }
    }
}
