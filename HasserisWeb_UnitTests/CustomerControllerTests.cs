using System;
using System.Collections.Generic;
using System.Linq;
using HasserisWeb;
using HasserisWeb.Controllers;
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
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: "Only_private_customers")
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


                Assert.That(result, Is.Not.Null);
            }
        }
        
        // new Test
        [Test]
        public void GetAllCustomers_OnDifferentCustomers_ReturnsAJsonObject()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: "Only_private_customers")
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
                // JObject jObj = JObject.Parse(json); kunne være nice hvis det virkede
                Console.WriteLine(json[0].Name);
             
                Assert.True(true);
                
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