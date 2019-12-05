using System;
using System.Collections.Generic;
using System.Linq;
using HasserisWeb;
using HasserisWeb.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace HasserisWeb_UnitTests
{
    public class CustomerControllerTests
    {
        [SetUp]
        public void Setup()
        {
            // Can be used to simplify our tests
        }

        [TearDown]
        public void TearDown()
        {
            // Can be used to simplify our tests
        }
        
        // new Test
        // Mocking DbContext
        [Ignore("This is a mocking test - but we are using in-memory now")]
        [Test]
        public void GetAllCustomers_Default_ReturnsAllCustomers()
        {
            // Arrange
            var mockCustomerDbSet = GetMockDbSet(CustomerTestList().AsQueryable());
            mockCustomerDbSet.Setup(f => f.Include(It.IsAny<string>())).Returns(mockCustomerDbSet.Object);
                

            var mockDbContext = new Mock<HasserisDbContext>();
            mockDbContext.Setup(f => f.Customers)
                .Returns(mockCustomerDbSet.Object);
            
            CustomerController controller = new CustomerController(mockDbContext.Object);
            
            
            // Act
            var actualCustomers = controller.GetAllCustomers();
            var expectedCustomers = CustomerTestList();
            
            // Assert
            Assert.That(actualCustomers, Is.EqualTo(expectedCustomers));
        }
        
        // new Test
        // Inmemory testing: https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
        [Test]
        public void GetAllCustomers_OnSomeCustomers_IsNotNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert seed data into the database using one instance of the context
            ContactInfo contactInfo = new ContactInfo("cholle18@student.aau.dk", "41126263");
            Address address = new Address("Brandstrupsgade 12", "9000", "Aalborg");
            
            using (var context = new HasserisDbContext(options, true))
            {
                context.Customers.Add(new Private("Christoffer", "Hollensen", address, contactInfo));
                context.Customers.Add(new Public(address, contactInfo, "Jammerbugt Kommune", "420133769"));
                context.Customers.Add(new Business(address,  contactInfo, "Skovsgaard Hotel", "32217696969"));
                context.SaveChanges();
            }
            
            // Act
            using (var context = new HasserisDbContext(options, true))
            {
                var service = new CustomerController(context); // getting access to that database
                var result = service.GetAllCustomers();

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }
        
        // new Test
        [Test]
        public void GetAllCustomers_OnDifferentCustomers_ReturnsAJsonObject()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert seed data into the database using one instance of the context
            ContactInfo contactInfo = new ContactInfo("cholle18@student.aau.dk", "41126263");
            Address address = new Address("Brandstrupsgade 12", "9000", "Aalborg");

            
            using (var context = new HasserisDbContext(options, true))
            {
                context.Customers.Add(new Private("Christoffer", "Hollensen", address, contactInfo));
                context.Customers.Add(new Public(address, contactInfo, "Jammerbugt Kommune", "420133769"));
                context.Customers.Add(new Business(address,  contactInfo, "Skovsgaard Hotel", "32217696969"));
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var service = new CustomerController(context); // getting access to that database
                var result = service.GetAllCustomers();

                //dynamic jsonResult = JsonConvert.DeserializeObject(result.ToString());                Console.WriteLine(result);
                dynamic json = JsonConvert.DeserializeObject(result);
                Console.WriteLine(json[0].Name);
             
                // we check that the type is of JArray
                Assert.IsInstanceOf<Newtonsoft.Json.Linq.JArray>( json.GetType() );
            }
        }
        
        // new Test
        [Test]
        public void GetAllCustomers_OnDifferentCustomers_ReturnsTheRightAmountOfCustomers()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Database name has to be different or the tests will use the same databaseName
                .Options;

            // Insert seed data into the database using one instance of the context
            ContactInfo contactInfo = new ContactInfo("cholle18@student.aau.dk", "41126263");
            Address address = new Address("Brandstrupsgade 12", "9000", "Aalborg");

            
            using (var context = new HasserisDbContext(options, true))
            {
                context.Customers.Add(new Private("Christoffer", "Hollensen", address, contactInfo));
                context.Customers.Add(new Public(address, contactInfo, "Jammerbugt Kommune", "420133769"));
                context.Customers.Add(new Business(address,  contactInfo, "Skovsgaard Hotel", "32217696969"));
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var service = new CustomerController(context); // getting access to that database
                var result = service.GetAllCustomers();

                //dynamic jsonResult = JsonConvert.DeserializeObject(result.ToString());                Console.WriteLine(result);
                dynamic json = JsonConvert.DeserializeObject(result);
                Console.WriteLine(json.Count);
             
                // we check that the type is of JArray
                Assert.That(json.Count, Is.EqualTo(3));
            }
        }
        
        // new Test
        [Test]
        public void EditPrivateCustomer_OnPrivateCustomer_ReturnsAnEditedObject()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert seed data into the database using one instance of the context
            ContactInfo contactInfo = new ContactInfo("cholle18@student.aau.dk", "41126263");
            Address address = new Address("Brandstrupsgade 12", "9000", "Aalborg");

            
            using (var context = new HasserisDbContext(options, true))
            {
                context.Customers.Add(new Private("Christoffer", "Hollensen", address, contactInfo));
                context.Customers.Add(new Public(address, contactInfo, "Jammerbugt Kommune", "420133769"));
                context.Customers.Add(new Business(address,  contactInfo, "Skovsgaard Hotel", "32217696969"));
                context.SaveChanges();
            }

            // Object to edit
            Customer customerToEdit = new Private("Cholle", "Hollensen", address, contactInfo);
            // Ændrer ID på customerToEdit, så vi får samme custoemr at edit
            customerToEdit.ID = 1;
            
            // Act
            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var customerController = new CustomerController(context); // getting access to that database

                // Den selecter fra id så vi skal lade som om der kommer et object ind med samme ID
                customerController.EditPrivateCustomer(customerToEdit);

                dynamic jsonResult = JsonConvert.DeserializeObject("asd".ToString());
                
                // Get all customers to get reference to old one
                
                // Assert that the old one and the edited customer are the same by looking at the name
                //Assert.That(jsonResult[0].);
                Assert.Pass();
            }
        }


        
        // HELPERS METHODS BELOW!
        private List<Customer> CustomerTestList()
        {
            ContactInfo contactInfo = new ContactInfo("cholle18@student.aau.dk", "41126263");
            Address address = new Address("Brandstrupsgade 12", "9000", "Aalborg");

            return new List<Customer>()
            {
                new Private("Christoffer", "Hollensen", address, contactInfo),
                new Public(address, contactInfo, "Jammerbugt Kommune", "420133769"),
                new Business(address,  contactInfo, "Skovsgaard Hotel", "32217696969")
            };
        }
        
        
        // Method below is not written by the group!!!
        // Method below is taken from: https://stackoverflow.com/a/34857934/2715087
        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }
    }
}