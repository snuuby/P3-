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
                Employee tempEmployee = new Employee("Christopher", "Chollesen", "AdminPlus", 120, new ContactInfo("Cholle17@gmail.com", "41126263"), new Address("Sohngaardsholmparken", "9000", "Aalborg", "Til højre"));
                tempEmployee.AddLoginInfo("Cholle", "Cholle17");
                Employee tempEmployee_one = new Employee("Jakob", "Østenkjær", "AdminPlus", 150, new ContactInfo("Jallehansen17@gmail.com", "28943519"), new Address("Herningvej", "9220", "Aalborg", "Til højre"));
                tempEmployee_one.AddLoginInfo("Snuuby", "Jakob17");
                Employee tempEmployee_two = new Employee("Kristian", "Eriksen", "AdminPlus", 150, new ContactInfo("Kesseeriksen@gmail.com", "50734649"), new Address("Herningvej", "9220", "Aalborg", "Til højre"));
                tempEmployee_two.AddLoginInfo("Kristian", "Kristian17");
                Employee tempEmployee_three = new Employee("Daniel", "Heilskov", "AdminPlus", 150, new ContactInfo("Daniellan99@gmail.com", "42438049"), new Address("Herningvej", "9220", "Aalborg", "Til højre"));
                tempEmployee_three.AddLoginInfo("Daniel", "Daniel17");
                Employee tempEmployee_four = new Employee("Simon", "Kanne", "AdminPlus", 150, new ContactInfo("Simonkanne@gmail.com", "61466211"), new Address("Herningvej", "9220", "Aalborg", "Til højre"));
                tempEmployee_four.AddLoginInfo("Simon", "Simon17");
                Employee tempEmployee_five = new Employee("Mathias", "Møller", "AdminPlus", 150, new ContactInfo("Mathiasmoellersoerensen@gmail.com", "93958200"), new Address("Herningvej", "9220", "Aalborg", "Til højre"));
                tempEmployee_five.AddLoginInfo("Mathias", "Møller17");
                Employee tempEmployee_six = new Employee("Andreas", "Nichum", "AdminPlus", 150, new ContactInfo("Anichu18@student.aau.dk", "24840884"), new Address("Herningvej", "9220", "Aalborg", "Til højre"));
                tempEmployee_six.AddLoginInfo("Andreas", "Andreas17");

                Customer tempCustomer = new Private("Erik", "Larsen", new Address("Aalborg Vej", "9220", "Aalborg", "Første dør til højre"), new ContactInfo("Erik@gmail.com", "23131313"));
                Customer tempCustomer_one = new Private("Lars", "Eriksen", new Address("Aalborghusvej", "9110", "Aalborg", "Anden dør"), new ContactInfo("lars@gmail.com", "23131313"));

                Furniture tempFurniture = new Furniture("Sofa møbel", 10, "Sofa", 10);

                Equipment testEquipment = new Vehicle("Stor lastbil", "Opel", "13131313");
                Equipment testEquipment_one = new Vehicle("Alimndelig bil", "Citroen", "13131313");



                List<DateTime> testList = new List<DateTime>() { new DateTime(2019, 11, 13), new DateTime(2019, 11, 14) };
                List<DateTime> testList_two = new List<DateTime>() { new DateTime(2019, 11, 03), new DateTime(2019, 11, 04) };

                Delivery tempDelivery = new Delivery("Test Delivery", tempCustomer, new Address("Hasseris vej", "9220", "Aalborg", "Tredje dør til venstre"), 600, testList, "Giv erik noget", "28313131", "Foam", 5);
                tempDelivery.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee_one, Task = tempDelivery });
                tempDelivery.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee, Task = tempDelivery });
                tempDelivery.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee_five, Task = tempDelivery });
                tempDelivery.taskAssignedEquipment.Add(new TaskAssignedEquipment() { Equipment = testEquipment, Task = tempDelivery });
                foreach (DateTime date in testList)
                {
                    PauseTimes temp = new PauseTimes();
                    temp.Date = date;
                    tempDelivery.PauseTimes.Add(temp);
                }

                Moving tempMoving = new Moving("Test Moving", tempCustomer_one, new Address("Kukux vej", "9000", "Aalborg", "første dør til venstre"), 700, testList_two, "Hjælp Lars med at flytte", "23131343", tempCustomer_one.Address, 5);
                tempMoving.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee_two, Task = tempMoving });
                tempMoving.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee_three, Task = tempMoving });
                tempMoving.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee_four, Task = tempMoving });
                tempMoving.taskAssignedEmployees.Add(new TaskAssignedEmployees() { Employee = tempEmployee_six, Task = tempMoving });
                tempMoving.taskAssignedEquipment.Add(new TaskAssignedEquipment() { Equipment = testEquipment_one, Task = tempMoving });
                tempMoving.Furnitures.Add(tempFurniture);
                foreach (DateTime date in testList_two)
                {
                    PauseTimes temp = new PauseTimes();
                    temp.Date = date;
                    tempMoving.PauseTimes.Add(temp);
                }





                db.Tasks.Add(tempDelivery);
                db.Tasks.Add(tempMoving);

                db.Employees.Add(tempEmployee);
                db.Employees.Add(tempEmployee_one);
                db.Employees.Add(tempEmployee_two);
                db.Employees.Add(tempEmployee_three);
                db.Employees.Add(tempEmployee_four);
                db.Employees.Add(tempEmployee_five);
                db.Employees.Add(tempEmployee_six);

                db.Customers.Add(tempCustomer);
                db.Customers.Add(tempCustomer_one);

                db.Equipment.Add(testEquipment);
                db.Equipment.Add(testEquipment_one);
                db.Furniture.Add(tempFurniture);




                db.SaveChanges();
                foreach (Task ctxTask in db.Tasks)
                {
                    Console.WriteLine($"{ctxTask.Name}:");
                    if (ctxTask.Customer is Private)
                    {
                        Console.WriteLine($"{ctxTask.Customer.ID}: {((Private)ctxTask.Customer).Firstname} {((Private)ctxTask.Customer).Lastname}");

                    }
                    else if (ctxTask.Customer is Business)
                    {
                        Console.WriteLine($"{ctxTask.Customer.ID}: {((Business)ctxTask.Customer).Name}");

                    }
                    else
                    {
                        Console.WriteLine($"{((Public)ctxTask.Customer).ID}: {((Public)ctxTask.Customer).Name}");

                    }
                    foreach (var employee in ctxTask.taskAssignedEmployees.Select(ta => ta.Employee))
                    {

                        Console.WriteLine($"{employee.ID}:  {employee.Firstname} {employee.Lastname}");
                    }
                    foreach (var equipment in ctxTask.taskAssignedEquipment.Select(e => e.Equipment))
                    {

                        Console.WriteLine($"{equipment.ID}: {equipment.Name}");
                    }
                    if (ctxTask is Moving)
                    {
                        Console.WriteLine($"Furniture:");

                        foreach (var furniture in ((Moving)ctxTask).Furnitures)
                        {

                            Console.WriteLine($"{furniture.ID}:  {furniture.Name}");
                        }
                    }
                    Console.WriteLine($"Date(s): ");

                    foreach (var date in ctxTask.Dates)
                    {

                        Console.WriteLine($"  {date.Date.DayOfYear}");
                    }

                }
                var test = db.Employees.FirstOrDefault();
                Console.WriteLine(test.ContactInfo.Email);
            }

        }






                db.Tasks.Add(tempDelivery);
                db.Tasks.Add(tempMoving);

                db.Employees.Add(tempEmployee);
                db.Employees.Add(tempEmployee_one);
                db.Employees.Add(tempEmployee_two);
                db.Employees.Add(tempEmployee_three);
                db.Employees.Add(tempEmployee_four);
                db.Employees.Add(tempEmployee_five);
                db.Employees.Add(tempEmployee_six);

                db.Customers.Add(tempCustomer);
                db.Customers.Add(tempCustomer_one);

                db.Equipment.Add(testEquipment);
                db.Equipment.Add(testEquipment_one);
                db.Furniture.Add(tempFurniture);

                /*
                DatabaseTester test = new DatabaseTester();
                try
                {
                    Employee jakob = HasserisDbContext.VerifyPassword("Jakob17", "Snuuby");
                    Debug.WriteLine(jakob.ID);
                    //Do something with Jakob here
                }
                catch (UnauthorizedAccessException)
                {
                    Debug.WriteLine("Wrong password");
                }
                catch (Exception)
                {
                    Debug.WriteLine("Likely wrong username");
                }
                */
            }


        }
    }

}
