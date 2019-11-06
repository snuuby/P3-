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
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Employee tempEmployee_one = new Employee("Jakob", "Østenkjær", "AdminPlus", 150, new ContactInfo("jallehansen17@gmail.com", "13131313"), new Address("Herningvej", "9220", "Aalborg", "Til højre"));
                tempEmployee_one.AddLoginInfo("Snuuby", "Jakob17");
                Equipment testEquipment = new Vehicle("Stor lastbil", "Model", "13131313");
                Employee tempEmployee = new Employee("Christopher", "Chollesen", "Admin", 120, new ContactInfo("Cholle17@gmail.com", "60701010"), new Address("Sohngaardsholmparken", "9000", "Aalborg", "Til højre"));
                tempEmployee.AddLoginInfo("Cholle", "Cholle17");
                Customer tempCustomer = new Private("Erik", "Larsen", new Address("Aalborg Vej", "9220", "Aalborg", "Første dør til højre"), new ContactInfo("Erik@gmail.com", "23131313"));
                List<DateTime> testList = new List<DateTime>() { new DateTime(2019, 05, 03), new DateTime(2019, 05, 04) };
                Delivery tempTask = new Delivery("Test Delivery", tempCustomer, new Address("Hasseris vej", "9220", "Aalborg", "Tredje dør til venstre"), 600, testList, "Giv erik noget", "28313131", "Foam", 5);
                tempTask.taskAssignedEmployees.Add(new TaskAssignedEmployees() {Employee = tempEmployee_one, Task = tempTask });
                tempTask.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee, Task = tempTask });
                tempTask.taskAssignedEquipment.Add(new TaskAssignedEquipment() { Equipment = testEquipment, Task = tempTask });
                foreach (DateTime date in testList)
                {
                    PauseTimes temp = new PauseTimes();
                    temp.Date = date;
                    tempTask.PauseTimes.Add(temp);
                }
                db.Tasks.Add(tempTask);
                db.Employees.Add(tempEmployee);
                db.Employees.Add(tempEmployee_one);
                db.Customers.Add(tempCustomer);
                db.Equipment.Add(testEquipment);


                db.SaveChanges();
                foreach (Task ctxTask in db.Tasks)
                {
                    Console.WriteLine($"{ctxTask.Name}:");
                    foreach (var employeename in ctxTask.taskAssignedEmployees.Select(ta => ta.Employee.Firstname))
                    {
                        Console.WriteLine(ctxTask.PauseTimes);
                        Console.WriteLine($"  {employeename}");
                    }
                }

            }

        }


    }
    
}
