using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace HasserisWeb.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        // Dependency injection by constructor
        public HasserisDbContext database;

        public EmployeeController(HasserisDbContext sc)
        {
            database = sc;
        }

        // Method to retrieve all employees for the employee overview in the view component.
        [Route("all")]
        public string GetAllEmployees()
        {
            return JsonConvert.SerializeObject(database.Employees.Include(address => address.Address).
                                                Include(contact => contact.ContactInfo).Include(address => address.Address).ToList());
        }
        [Route("available")]
        public string GetAvailableEmployees()
        {
            return JsonConvert.SerializeObject(database.Employees.
                Where(employee => employee.IsAvailable && (employee.Type == "Admin" || employee.Type == "AdminPlus")).
                Include(contact => contact.ContactInfo).Include(address => address.Address).ToList());
        }

        // Method to retrieve a specific employee by ID
        [Route("{id}")]
        public string GetSpecificEmployee(int id)
        {
            return JsonConvert.SerializeObject(database.Employees
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }
        // add business customer
        [HttpPost]
        [Route("add")]
        public void CreateEmployee([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // General employee information
            string employeeFirstName = temp.Firstname;
            string employeeLastName = temp.Lastname;
            string employeeType = temp.Type;
            double employeeWage = temp.Wage;
            //Address
            string employeeLivingAddress = temp.LivingAddress;
            string employeeZIP = temp.ZIP;
            string employeeCity = temp.City;
            string employeeNote = temp.Note;
            //ContactInfo
            string employeeEmail = temp.Email;
            string employeePhoneNumber = temp.Phonenumber;


            Employee tempEmployee = new Employee(employeeFirstName, employeeLastName, employeeType, employeeWage,
                new ContactInfo(employeeEmail, employeePhoneNumber),
                new Address(employeeLivingAddress, employeeZIP, employeeCity, employeeNote));
            string available = temp.Available;
            if (available == "Yes")
            {
                tempEmployee.IsAvailable = true;
            }
            else
            {
                tempEmployee.IsAvailable = false;
            }
            database.Employees.Add(tempEmployee);

            database.SaveChanges();
        }
        [HttpPost]
        [Route("edit")]
        public void EditEmployee([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // General employee information
            int id = temp.ID;
            Employee tempEmployee = database.Employees.FirstOrDefault(e => e.ID == id);
            tempEmployee.Firstname = temp.Firstname;
            tempEmployee.Lastname = temp.Lastname;
            tempEmployee.Type = temp.Type;
            tempEmployee.Wage = temp.Wage;
            //Address
            string livingaddress = temp.LivingAddress;
            string zip = temp.ZIP;
            string city = temp.City;
            Address address = new Address(livingaddress, zip, city);
            tempEmployee.Address = address;
            //ContactInfo
            string email = temp.Email;
            string phonenumber = temp.Phonenumber;
            ContactInfo contactinfo = new ContactInfo(email, phonenumber);
            tempEmployee.ContactInfo = contactinfo;
            string available = temp.Available;
            if (available == "Yes")
            {
                tempEmployee.IsAvailable = true;
            }
            else
            {
                tempEmployee.IsAvailable = false;
            }
            database.Employees.Update(tempEmployee);
            database.SaveChanges();
        }
    }
}
