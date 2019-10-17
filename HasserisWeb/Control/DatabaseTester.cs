using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HasserisWeb
{
    public class DatabaseTester
    {

        public DatabaseTester()
        {
            //CreatePeopleTester();
            
            LoadPeopleTester();
            DatabaseTestDebugger();

        }
        private void CreatePeopleTester()
        {
            Private privateCustomer = new Private("Jakob", "Hansen", "Private",
                new Address("myrdalstrade", "9220", "Aalborg", "1. sal t.h"),
                new ContactInfo("jallehansen17/gmail.com", "28943519"));
            HasserisDbContext.SaveElementToDatabase<Private>(privateCustomer);


            Employee employee_one = new Employee("Anders", "Andreasen","Employee", 180,
                new ContactInfo("andreas/gmail.com", "223313145"),
                new Address("Andreasensvej", "9220", "Aalborg", "Anden etache"));
            employee_one.AddLoginInfo("Snuuby", "Jakob17");

            Employee employee_two = new Employee("Peter", "Kukukson", "Admin", 190,
                new ContactInfo("Peter/gmail.com", "123123123"),
                new Address("Petersvej", "9220", "Aalborg", "Tredje Etache"));

            employee_two.AddLoginInfo("Cholle", "Christopher18");
            HasserisDbContext.SaveElementToDatabase<Employee>(employee_one);
            HasserisDbContext.SaveElementToDatabase<Employee>(employee_two);

            Vehicle vehicle = new Vehicle("Stor bil", "Vehicle", "Opel", "12312123");
            HasserisDbContext.SaveElementToDatabase<Vehicle>(vehicle);

            Delivery delivery = new Delivery("testDelivery", "Delivery", privateCustomer,
                new Address("myrdal", "2", "aalborg", "testnote"), 1000, new List<DateTime>() { new DateTime(2019, 12, 3), new DateTime(2019, 1, 5) }, "testnote", "22331133", "Foam", 2);
            delivery.AddElementToTask(employee_one);
            delivery.AddElementToTask(employee_two);
            delivery.AddElementToTask(vehicle);
            HasserisDbContext.SaveElementToDatabase<Delivery>(delivery);






        }
        public void LoadPeopleTester()
        {
            //PrivateCustomer add

            SystemControl.calendar.AddTask((Delivery)HasserisDbContext.LoadElementFromDatabase("Delivery", 1));



        }
        
        private void DatabaseTestDebugger()
        {
            Debug.WriteLine(SystemControl.calendar.name);
            foreach (Task task in SystemControl.calendar.tasks)
            {
                Debug.WriteLine("ID: " + task.id);
                Debug.WriteLine("Name: " + task.name);
                Debug.WriteLine("Assigned Employee(s): ");
                foreach(Employee employee in task.assignedEmployees)
                {
                    Debug.WriteLine("Employee ID: " + employee.id.ToString());
                    Debug.WriteLine("   Firstname: " + employee.firstName);
                    Debug.WriteLine("   Lastname: " + employee.lastName);
                    Debug.WriteLine("   Type: " + employee.type);
                }
                Debug.WriteLine("");
                Debug.WriteLine("Assigned Equipment(s): ");
                foreach (Equipment equipment in task.assignedEquipment)
                {
                    if (equipment is Vehicle)
                    {
                        Debug.WriteLine("Vehicle ID: " + equipment.id.ToString());
                        Debug.WriteLine("   Name: " + equipment.name);
                        Debug.WriteLine("   Model: " + ((Vehicle)equipment).model);
                        Debug.WriteLine("   Plates: " + ((Vehicle)equipment).regNum);
                    }
                    else if (equipment is Tool)
                    {
                        Debug.WriteLine("Tool ID: " + equipment.id.ToString());
                        Debug.WriteLine("   Name: " + equipment.name);
                    }
                }
                Debug.WriteLine("");
                Debug.WriteLine("Assigned Customer(s): ");

                if (task.assignedCustomer is Private)
                {
                    Debug.WriteLine("Private customer ID: " + task.assignedCustomer.id.ToString());
                    Debug.WriteLine("   Firstname: " + ((Private)task.assignedCustomer).firstName);
                    Debug.WriteLine("   Lastname: " + ((Private)task.assignedCustomer).lastName);
                    Debug.WriteLine("   Type: " + ((Private)task.assignedCustomer).type);
                }
                else if (task.assignedCustomer is Public)
                {
                    Debug.WriteLine("Public customer ID: " + task.assignedCustomer.id.ToString());
                    Debug.WriteLine("   Name: " + ((Public)task.assignedCustomer).businessName);
                    Debug.WriteLine("   EAN: " + ((Public)task.assignedCustomer).EAN);
                    Debug.WriteLine("   Type: " + ((Private)task.assignedCustomer).type);
                }
                else if (task.assignedCustomer is Business)
                {
                    Debug.WriteLine("Business customer ID: " + task.assignedCustomer.id.ToString());
                    Debug.WriteLine("   Name: " + ((Business)task.assignedCustomer).businessName);
                    Debug.WriteLine("   CVR: " + ((Business)task.assignedCustomer).CVR);
                    Debug.WriteLine("   Type: " + ((Business)task.assignedCustomer).type);
                }
            }


    
        }


    

    }

}
