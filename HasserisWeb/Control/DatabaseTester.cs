using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HasserisWeb
{
    public class DatabaseTester
    {
        public static List<Appointment> appointments = new List<Appointment>();
        public static List<Equipment> equipments = new List<Equipment>();
        public static List<Employee> employees = new List<Employee>();
        public static List<Customer> customers = new List<Customer>();

        public DatabaseTester()
        {
            CreatePeopleTester();
            //LoadPeopleTester();
            DatabaseTestDebugger();

        }
        private void CreatePeopleTester()
        {
            PrivateCustomer privateCustomer = new PrivateCustomer("Jakob", "Hansen", "Private",
                new Address("myrdalstrade", "9220", "Aalborg", "1. sal t.h"),
                new ContactInfo("jallehansen17/gmail.com", "28943519"));
            customers.Add(privateCustomer);

            Delivery delivery = new Delivery("testDelivery", "Delivery", 10000, privateCustomer,
                new Address("myrdal", "2", "aalborg", "testnote"), 1000, new DateTime(2019, 3, 12), "testnote", "22331133", "Foam", 2);
            appointments.Add(delivery);

            Employee employee_one = new Employee("Anders", "Andreasen", 180,
                new ContactInfo("andreas/gmail.com", "223313145"),
                new Address("Andreasensvej", "9220", "Aalborg", "Anden etache"));
            employees.Add(employee_one);

            Employee employee_two = new Employee("Peter", "Kukukson", 190,
                new ContactInfo("Peter/gmail.com", "123123123"),
                new Address("Petersvej", "9220", "Aalborg", "Tredje Etache"));
            employees.Add(employee_two);

            Vehicle vehicle = new Vehicle("Stor bil", "Lastbil", "Opel", "12312123");
            equipments.Add(vehicle);

            delivery.AddElementToAppointment(employee_one);
            delivery.AddElementToAppointment(employee_two);
            delivery.AddElementToAppointment(vehicle);

        }
        public void LoadPeopleTester()
        {
            //PrivateCustomer add
            customers.Add((PrivateCustomer)HasserisDbContext.LoadElementFromDatabase("Private", 1));
            appointments.Add((Delivery)HasserisDbContext.LoadElementFromDatabase("Delivery", 1));
            employees.Add((Employee)HasserisDbContext.LoadElementFromDatabase("Employee", 1));
            employees.Add((Employee)HasserisDbContext.LoadElementFromDatabase("Employee", 2));
            equipments.Add((Vehicle)HasserisDbContext.LoadElementFromDatabase("Vehicle", 1));


        }
        private void DatabaseTestDebugger()
        {
            foreach (Appointment appointment in appointments)
            {
                Debug.WriteLine(appointment.name);
                Debug.WriteLine("Assigned Employee(s): ");
                foreach(Employee employee in employees)
                {
                    Debug.WriteLine("ID: " + employee.id.ToString());
                    Debug.WriteLine("   Firstname: " + employee.firstName);
                    Debug.WriteLine("   Lastname: " + employee.lastName);
                }
                Debug.WriteLine("");
                Debug.WriteLine("Assigned Equipment(s): ");
                foreach (Equipment equipment in equipments)
                {
                    Debug.WriteLine("ID: " + equipment.id.ToString());
                    Debug.WriteLine("   Name: " + equipment.name);
                }
                Debug.WriteLine("");
                Debug.WriteLine("Assigned Customer(s): ");
                foreach (Customer customer in customers)
                {
                    Debug.WriteLine("ID: " + customer.id.ToString());
                    Debug.WriteLine("   Firstname: " + customer.firstName);
                    Debug.WriteLine("   Lastname: " + customer.lastName);
                    Debug.WriteLine("   Type: " + customer.type);
                }
            }



        }




    }
}
