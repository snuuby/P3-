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
        public void CreateVehicle([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            string vehicleName = temp.Name;
            string vehicleModel = temp.Model;
            string vehicleRegNum = temp.RegNum;

            Vehicle vehicle = new Vehicle(vehicleName, vehicleModel, vehicleRegNum);
            string available = temp.Available;
            if (available == "Yes")
            {
                vehicle.IsAvailable = true;
            }
            else
            {
                vehicle.IsAvailable = false;
            }
            database.Equipment.Add(vehicle);
            database.SaveChanges();
        }
        [Route("edit")]
        public void EditVehicle([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int id = temp.ID;
            Vehicle vehicle = (Vehicle)database.Equipment.FirstOrDefault(v => v.ID == id);
            vehicle.Name= temp.Name;
            vehicle.Model = temp.Model;
            vehicle.RegNum = temp.RegNum;
            string available = temp.Available;
            if (available == "Yes")
            {
                vehicle.IsAvailable = true;
            }
            else
            {
                vehicle.IsAvailable = false;
            }

            database.Equipment.Update(vehicle);
            database.SaveChanges();
        }
    }
}
