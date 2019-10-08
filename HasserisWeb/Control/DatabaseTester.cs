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
            SystemControl.customers.Add(privateCustomer);

            Delivery delivery = new Delivery("testDelivery", "Delivery", privateCustomer,
                new Address("myrdal", "2", "aalborg", "testnote"), 1000, new List<DateTime>() { new DateTime(2019, 12, 3), new DateTime(2019, 1, 5) }, "testnote", "22331133", "Foam", 2);
            SystemControl.appointments.Add(delivery);
            SystemControl.calendar.AddAppointment(delivery);

            Employee employee_one = new Employee("Anders", "Andreasen","Employee", 180,
                new ContactInfo("andreas/gmail.com", "223313145"),
                new Address("Andreasensvej", "9220", "Aalborg", "Anden etache"));
            SystemControl.employees.Add(employee_one);

            Employee employee_two = new Employee("Peter", "Kukukson", "Admin", 190,
                new ContactInfo("Peter/gmail.com", "123123123"),
                new Address("Petersvej", "9220", "Aalborg", "Tredje Etache"));
            SystemControl.employees.Add(employee_two);

            Vehicle vehicle = new Vehicle("Stor bil", "Lastbil", "Opel", "12312123");
            SystemControl.equipment.Add(vehicle);

            delivery.AddElementToAppointment(employee_one);
            delivery.AddElementToAppointment(employee_two);
            delivery.AddElementToAppointment(vehicle);

        }
        public void LoadPeopleTester()
        {
            //PrivateCustomer add
            SystemControl.customers.Add((Private)HasserisDbContext.LoadElementFromDatabase("Private", 1));
            SystemControl.appointments.Add((Delivery)HasserisDbContext.LoadElementFromDatabase("Delivery", 1));
            SystemControl.calendar.AddAppointment((Delivery)HasserisDbContext.LoadElementFromDatabase("Delivery", 1));
            SystemControl.employees.Add((Employee)HasserisDbContext.LoadElementFromDatabase("Employee", 1));
            SystemControl.employees.Add((Employee)HasserisDbContext.LoadElementFromDatabase("Employee", 2));
            SystemControl.equipment.Add((Vehicle)HasserisDbContext.LoadElementFromDatabase("Vehicle", 1));


        }
        private void DatabaseTestDebugger()
        {
            Debug.WriteLine(SystemControl.calendar.name);
            foreach (Appointment appointment in SystemControl.calendar.appointments)
            {
                Debug.WriteLine("ID: " + appointment.id);
                Debug.WriteLine("Name: " + appointment.name);
                Debug.WriteLine("Assigned Employee(s): ");
                foreach(Employee employee in SystemControl.employees)
                {
                    Debug.WriteLine("Employee ID: " + employee.id.ToString());
                    Debug.WriteLine("   Firstname: " + employee.firstName);
                    Debug.WriteLine("   Lastname: " + employee.lastName);
                    Debug.WriteLine("   Type: " + employee.type);
                }
                Debug.WriteLine("");
                Debug.WriteLine("Assigned Equipment(s): ");
                foreach (Equipment equipment in SystemControl.equipment)
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
                foreach (Customer customer in SystemControl.customers)
                {
                    if (customer is Private)
                    {
                        Debug.WriteLine("Private customer ID: " + customer.id.ToString());
                        Debug.WriteLine("   Firstname: " + ((Private)customer).firstName);
                        Debug.WriteLine("   Lastname: " + ((Private)customer).lastName);
                        Debug.WriteLine("   Type: " + ((Private)customer).type);
                    }
                    else if (customer is Public)
                    {
                        Debug.WriteLine("Public customer ID: " + customer.id.ToString());
                        Debug.WriteLine("   Name: " + ((Public)customer).businessName);
                        Debug.WriteLine("   EAN: " + ((Public)customer).EAN);
                        Debug.WriteLine("   Type: " + ((Private)customer).type);
                    }
                    else if (customer is Business)
                    {
                        Debug.WriteLine("Business customer ID: " + customer.id.ToString());
                        Debug.WriteLine("   Name: " + ((Business)customer).businessName);
                        Debug.WriteLine("   CVR: " + ((Business)customer).CVR);
                        Debug.WriteLine("   Type: " + ((Business)customer).type);
                    }

                }
            }



        }




    }
}
