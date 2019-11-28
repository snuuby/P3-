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
    [Route("Vehicles")]
    public class VehicleController : Controller
    {
        public HasserisDbContext database;
        public VehicleController(HasserisDbContext sc)
        {
            database = sc;
        }

        [Route("all")]
        public string GetAllVehicles()
        {
                return JsonConvert.SerializeObject(database.Equipment.OfType<Vehicle>().ToList());
        }
        [Route("available")]
        public string GetAvailableVehicles()
        {
            return JsonConvert.SerializeObject(database.Equipment.OfType<Vehicle>().
                                            Where(car => car.IsAvailable).ToList());
        }

        [Route("{id}")]
        public string GetSpecificVehicle(int id)
        {
            return JsonConvert.SerializeObject(database.Equipment.OfType<Vehicle>()
                .FirstOrDefault(c => c.ID == id));
        }

        [Route("add")]
        public string CreateVehicle([FromBody]dynamic json)
        {
            dynamic eNewVehicle = JsonConvert.DeserializeObject(json.ToString());
            string vehicleName = eNewVehicle.newVehicle.name;
            string vehicleModel = eNewVehicle.newVehicle.model;
            string vehicleRegNum = eNewVehicle.newVehicle.regnum;

            Vehicle vehicle = new Vehicle(vehicleName, vehicleModel, vehicleRegNum);

            database.Equipment.Add(vehicle);
            database.SaveChanges();

            return "succesfully added new vehicle";
        }
    }
}
