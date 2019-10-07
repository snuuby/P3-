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
            Delivery delivery = new Delivery("testDelivery", "Delivery", 10000,
                new PrivateCustomer("jakob", "hansen", "Private",
                    new Address("myrdal", "2", "aalborg", "testnote"),
                    new ContactInfo("hansen/gmail", "2233")),
                new Address("myrdal", "2", "aalborg", "testnote"), 1000, new DateTime(2019, 3, 12), "testnote", "22331133", "Foam", 2);
            HasserisDbContext.SaveElementToDatabase<Delivery>(delivery);

            Employee employee = new Employee("Anders", "Andreasen", 180,
                new ContactInfo("andreas/gmail.com", "223313145"),
                new Address("Andreasensvej", "9220", "Aalborg", "Anden etache"));
            HasserisDbContext.SaveElementToDatabase<Employee>(employee);

            PrivateCustomer privateCustomer = new PrivateCustomer("Jakob", "Hansen", "Private",
                new Address("myrdalstræde", "9220", "Aalborg", "1. sal t.h"),
                new ContactInfo("jallehansen17/gmail.com", "28943519"));
            HasserisDbContext.SaveElementToDatabase<PrivateCustomer>(privateCustomer);

            Vehicle vehicle = new Vehicle("Stor bil", "Lastbil", "Opel", "12312123");
            HasserisDbContext.SaveElementToDatabase<Vehicle>(vehicle);
            delivery.AddElementToAppointment(employee);
            delivery.AddElementToAppointment(vehicle);
            dynamic databaseEmployees = HasserisDbContext.LoadAllOfSpecificElementFromDatabase("Employee");
            List<Employee> employees = (List<Employee>)databaseEmployees;
            List<Equipment> databaseEquipment = HasserisDbContext.LoadAllOfSpecificElementFromDatabase("Equipment");
            List<Appointment> databaseAppointments = HasserisDbContext.LoadAllOfSpecificElementFromDatabase("Appointment");
            List<Customer> databaseCustomers = HasserisDbContext.LoadAllOfSpecificElementFromDatabase("Customer");

            Debug.WriteLine(databaseEmployees[0].id + ": " + databaseEmployees[0].firstName + " " + databaseEmployees[0].lastName + ": " + databaseEmployees[0].appointmentIdString);
            Debug.WriteLine(databaseEquipment[0].id + ": " + databaseEquipment[0].name + ": " + databaseEquipment[0].appointmentIdString);
            Debug.WriteLine(databaseAppointments[0].id + ": " + databaseAppointments[0].name + ": " + databaseAppointments[0].employeesIdString + " " + databaseAppointments[0].equipmentsIdString);
            Debug.WriteLine(databaseCustomers[0].id + ": " + databaseCustomers[0].firstName + " " + databaseCustomers[0].lastName );


        }

    }
}
