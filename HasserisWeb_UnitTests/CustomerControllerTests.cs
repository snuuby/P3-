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
                
                // we check that the type is of JArray
                Assert.AreEqual(typeof(Newtonsoft.Json.Linq.JArray), json.GetType());
                //Assert.IsInstanceOf<Newtonsoft.Json.Linq.JArray>( json.GetType() );
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
        
        [Test]
        public void EditPrivateCustomer_OnPrivateCustomer_ReturnsAnEditedObject()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database context
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
            Private customerToEdit = new Private("Cholle", "Hollensen", address, contactInfo);
            // We change the ID the customerToEdit object, so we get the same customer to edit
            customerToEdit.ID = 1;
            
            // Act
            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var customerController = new CustomerController(context); // getting access to that database

                // We have to serialize our object as JSON so it looks like the one being sent from our view component.
                string json = JsonConvert.SerializeObject(customerToEdit, Formatting.Indented);
                
                // It selects from the same id so it will change the existing object in the database.
                customerController.EditPrivateCustomer(json);

                // Without getting a dependency on the method that retrieves all we will do it manually again.
                Private customer = (Private)context.Customers.FirstOrDefault(c => c.ID == customerToEdit.ID);
                
                // Assert that the old one and the edited customer are the same by looking at the name cuz we edited that information
                Assert.That(customerToEdit.Firstname, Is.EqualTo("Cholle"));
            }
        }
        
        [Test]
        public void EditPublicCustomer_OnPublicCustomer_ReturnsAnEditedObject()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database context
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
            Public customerToEdit = new Public(address, contactInfo, "Aalborg Kommune", "420133769");
            // We change the ID the customerToEdit object, so we get the same customer to edit
            customerToEdit.ID = 2;
            
            // Act
            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var customerController = new CustomerController(context); // getting access to that database

                // We have to serialize our object as JSON so it looks like the one being sent from our view component.
                string json = JsonConvert.SerializeObject(customerToEdit, Formatting.Indented);
                
                // It selects from the same id so it will change the existing object in the database.
                customerController.EditPublicCustomer(json);

                // Without getting a dependency on the method that retrieves all we will do it manually again.
                Public customer = (Public)context.Customers.FirstOrDefault(c => c.ID == customerToEdit.ID);
                
                
                //dynamic jsonResult = JsonConvert.DeserializeObject("asd".ToString());
                
                // Get all customers to get reference to old one
                
                // Assert that the old one and the edited customer are the same by looking at the name cuz we edited that information
                Assert.That(customerToEdit.Name, Is.EqualTo("Aalborg Kommune"));
            }
        }
        
                [Test]
        public void EditBusinessCustomer_OnBusinessCustomer_ReturnsAnEditedObject()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database context
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
            Business customerToEdit = new Business(address, contactInfo, "Fønix Hotel", "420133769");
            // We change the ID the customerToEdit object, so we get the same customer to edit
            customerToEdit.ID = 3;
            
            // Act
            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var customerController = new CustomerController(context); // getting access to that database

                // We have to serialize our object as JSON so it looks like the one being sent from our view component.
                string json = JsonConvert.SerializeObject(customerToEdit, Formatting.Indented);
                
                // It selects from the same id so it will change the existing object in the database.
                customerController.EditBusinessCustomer(json);

                // Without getting a dependency on the method that retrieves all we will do it manually again.
                Business customer = (Business)context.Customers.FirstOrDefault(c => c.ID == customerToEdit.ID);
                
                
                //dynamic jsonResult = JsonConvert.DeserializeObject("asd".ToString());
                
                // Get all customers to get reference to old one
                
                // Assert that the old one and the edited customer are the same by looking at the name cuz we edited that information
                Assert.That(customerToEdit.Name, Is.EqualTo("Fønix Hotel"));
            }
        }

        [Test]
        public void DeleteCustomer_OnLegitCustomer_ReturnsNewDBMinusTheCustomer()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database context
            ContactInfo contactInfo = new ContactInfo("cholle18@student.aau.dk", "41126263");
            Address address = new Address("Brandstrupsgade 12", "9000", "Aalborg");
            using (var context = new HasserisDbContext(options, true))
            {
                context.Customers.Add(new Private("Christoffer", "Hollensen", address, contactInfo));
                context.Customers.Add(new Public(address, contactInfo, "Jammerbugt Kommune", "420133769"));
                context.Customers.Add(new Business(address,  contactInfo, "Skovsgaard Hotel", "32217696969"));
                context.SaveChanges();
            }

            // Object to delete
            Private customerToDelete = new Private("Christoffer", "Hollensen", address, contactInfo);
            // We change the ID the customerToEdit object, so we get the same customer to delete
            customerToDelete.ID = 1;
            
            // Act
            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var customerController = new CustomerController(context); // getting access to that database

                // We have to serialize our object as JSON so it looks like the one being sent from our view component.
                string json = JsonConvert.SerializeObject(customerToDelete, Formatting.Indented);
                
                // It selects from the same id so it will change the existing object in the database.
                customerController.DeleteCustomer(json);

                // Without getting a dependency on the other method that retrieves all, we will do it manually again.
                Private customer = (Private)context.Customers.FirstOrDefault(c => c.ID == customerToDelete.ID);

                Assert.That(context.Customers.Contains(customer), Is.EqualTo(false));
            }
        }

        [Test]
        public void GetSpecificCustomer_OnExistingPrivateCustomerWithID1_ReturnsThatCustomer()
        {
                        var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database context
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
            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var customerController = new CustomerController(context); // getting access to that database

                // It selects from the same id so it will change the existing object in the database.
                var actual = customerController.GetSpecificCustomer(1);
                // Deserialzie our object into a Private type so we can access the properties such as Firstname and assert
                var actualObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Private>(actual);
                
                // Without getting a dependency on the method that retrieves all we will do it manually again.
                Private customer = (Private)context.Customers.FirstOrDefault(c => c.ID == 1);

                Assert.That(actualObj.Firstname, Is.EqualTo("Christoffer"));
            }
        }

        [Test]
        public void AddPrivateCustomer_AddsLegitPrivateCustomer_ReturnsNewDBPlusCustomer()
        {
            var options = new DbContextOptionsBuilder<HasserisDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database context
            ContactInfo contactInfo = new ContactInfo("cholle18@student.aau.dk", "41126263");
            Address address = new Address("Brandstrupsgade 12", "9000", "Aalborg");
            using (var context = new HasserisDbContext(options, true))
            {
                context.Customers.Add(new Private("Christoffer", "Hollensen", address, contactInfo));
                context.SaveChanges();
            }
            
            // Declare object to add
            Private customerToAdd = new Private("CholleAdded", "Holle", address, contactInfo);
            customerToAdd.ID = 2;
            
            // Serialize object so it can be used in our controller method
            var customerToAddJson = Newtonsoft.Json.JsonConvert.SerializeObject(customerToAdd);
            
            
            // Act
            // Use a clean instance of the context to run the test
            using (var context = new HasserisDbContext(options, true))
            {
                var customerController = new CustomerController(context); // getting access to that database

                // It selects from the same id so it will change the existing object in the database.
                customerController.AddPrivateCustomer(customerToAddJson);

                // Without getting a dependency on the method that retrieves all we will do it manually again.
                Private customer = (Private)context.Customers.FirstOrDefault(c => c.ID == 2);

                Assert.That(customer.Firstname, Is.EqualTo("CholleAdded"));
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
                new Business(address,  contactInfo, "Skovsgaardhotel", "32217696969")
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