using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace HasserisWeb.Controllers
{
    [Route("furnitures")]
    public class FurnituresController : Controller
    {
        public HasserisDbContext database;
        public FurnituresController(HasserisDbContext sc)
        {
            database = sc;
        }

        [Route("all")]
        public string GetAllFurnitures()
        {
            return JsonConvert.SerializeObject(database.Furniture.ToList());

        }

        [Route("{id}")]
        public string GetSpecificFurniture(int id)
        {
            return JsonConvert.SerializeObject(database.Furniture
                .FirstOrDefault(c => c.ID == id));
        }

        [Route("add")]
        public string CreateFurniture([FromBody]dynamic json)
        {
            dynamic eNewFurniture = JsonConvert.DeserializeObject(json.ToString());
            string furnitureName = eNewFurniture.newFurniture.name;
            double furnitureCubicSize = eNewFurniture.newFurniture.cubicSize;
            string furnitureType = eNewFurniture.newFurniture.type;
            double furnitureWeight = eNewFurniture.newFurniture.weight;

            Furniture furniture = new Furniture(furnitureName, furnitureCubicSize, furnitureType, furnitureWeight);

            database.Furniture.Add(furniture);
            database.SaveChanges();

            return "Succesfully added new tool";

        }
    }
}
