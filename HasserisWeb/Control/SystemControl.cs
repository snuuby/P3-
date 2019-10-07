using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HasserisWeb
{
    public class SystemControl
    {
        public SystemControl()
        {
            // Creating new instance of Employee
            Employee testEmployee = new Employee("FirstName", "LastName", 420.00,
                new ContactInfo("fakemail@taxi.com", "TLF"),
                new Address("vejnavn", "ZIP", "City", "NOTE"));

            // Creating new instance of Business
            Business testBusinessCustomer = new Business("FirstName", "LastName", "Type",
                new Address("VejNavn", "ZIP", "City", "NOTE"),
                new ContactInfo("fakemail@taxi.com", "TLF"), 
                "businessName", "CVR");

            // Creating new instance of Public
            Public testPublicCustomer = new Public("FirstName", "LastName", "Type",
                new Address("VejNavn", "ZIP", "City", "NOTE"),
                new ContactInfo("fakemail@taxi.com", "TLF"),
                "businessName", "EAN");

            // Creating new instance of Delivery
            Delivery testDelivery = new Delivery("Name", "Type", 10000.0,
                new PrivateCustomer("FirstName", "LastName", "Type",
                    new Address("Vejnavn", "ZIP", "City", "NOTE"),
                    new ContactInfo("fakemail@taxi.com", "TLF")),
                new Address("Vejnavn", "ZIP", "City", "NOTE"), 9001.11, new DateTime(2019, 7, 10), "note", "workPhoneNumber", "material", 30);

            // Creating new instance of Moving
            Moving testMoving = new Moving("Name", "type", 1000.0,
                new PrivateCustomer("FirstName", "LastName", "Type",
                    new Address("Vejnavn", "ZIP", "City", "NOTE"),
                    new ContactInfo("fakemail@taxi.com", "TLF")),
                new Address("Vejnavn", "ZIP", "City", "note"), 90001.11, new DateTime(2019, 7, 10), "note", "workPhoneNumber",
                new Address("Vejnavn", "ZIP", "City", "note"), 30);

            // Creating new instance of Tool
            Tool testTool = new Tool("Name", "type");

            // Creating new instance of Vehicle
            Vehicle testVehicle = new Vehicle("Name", "Type", "model", "RegistrationNumber");

            // Creating new instance of Calender
            Calender testCalender = new Calender();

            // Creating new instance of Furniture
            Furniture testFurniture = new Furniture("NameofFurniture", 0.3, "type", 2.5);


        }

    }
}
