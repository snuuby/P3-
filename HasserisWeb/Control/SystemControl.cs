using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;

namespace HasserisWeb
{
    public class SystemControl
    {
        
        public SystemControl()
        {

            using (var db = new HasserisDbContext())
            {
                var employee = db.Employees.FirstOrDefault();
                employee.AddLoginInfo("Snuuby", "Jakob17");
                Equipment testEquipment = new Vehicle("Stor lastbil", "Vehicle", "Model", "13131313");
                Employee tempEmployee = new Employee("Christopher", "Chollesen", "Admin", 120, new ContactInfo("Cholle17@gmail.com", "60701010"), new Address("Sohngaardsholmparken", "9000", "Aalborg", "Til højre"));
                Customer tempCustomer = new Private("Erik", "Larsen", "Private", new Address("Aalborg Vej", "9220", "Aalborg", "Første dør til højre"), new ContactInfo("Erik@gmail.com", "23131313"));
                List<DateTime> testList = new List<DateTime>() { new DateTime(2019, 05, 03), new DateTime(2019, 05, 04) };
                Delivery tempTask = new Delivery("Test Delivery", "Delivery", tempCustomer, new Address("Hasseris vej", "9220", "Aalborg", "Tredje dør til venstre"), 600, testList, "Giv erik noget", "28313131", "Foam", 5);
                tempTask.taskAssignedEmployees.Add(new TaskAssignedEmployees() {Employee = employee, Task = tempTask });
                tempTask.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee, Task = tempTask });
                tempTask.taskAssignedEquipment.Add(new TaskAssignedEquipment() { Equipment = testEquipment, Task = tempTask });
                db.Employees.Update(employee);
                db.SaveChanges();

            }

        }


    }
    
}
