﻿using Microsoft.EntityFrameworkCore;

namespace HasserisWeb
{

    public class HasserisDbContext : DbContext
    {
        // Sets testing equals to false by default
        private bool testing = false;


        public HasserisDbContext(DbContextOptions<HasserisDbContext> options, bool testing)
            : base(options)
        {
            // setting testing. This is done because of our unit tests
            this.testing = testing;
        }

        public HasserisDbContext() : base()
        {

        }



        public DbSet<InspectionReport> Inspections { get; set; }
        public DbSet<Offer> Offers { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // er den her nødvendig hvis Options bliver sat i StartUp.cs. Den fucker nemlig mine unit tests op, fordi den kører ved hver creation af DbContext.
        // ^nvm har fundet et andet fix ved at sætte en testing bool
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!testing)
            {
                optionsBuilder.UseSqlite("Data Source=Database/HasserisDatabase.db;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Sub classes of Task
            modelBuilder.Entity<Delivery>();
            modelBuilder.Entity<Moving>();

            //Sub classes of Customer
            modelBuilder.Entity<Private>();
            modelBuilder.Entity<Public>();
            modelBuilder.Entity<Business>();

            //Sub classes of Equipment
            modelBuilder.Entity<Vehicle>();
            modelBuilder.Entity<Tool>();



            modelBuilder.Entity<Address>();

            modelBuilder.Entity<ContactInfo>();
            modelBuilder.Entity<DateTimes>();
            modelBuilder.Entity<PauseTimes>();

            modelBuilder.Entity<TaskAssignedEmployees>()
                    .HasKey(e => new { e.EmployeeID, e.TaskID });
            modelBuilder.Entity<TaskAssignedEquipment>()
                    .HasKey(e => new { e.EquipmentID, e.TaskID });



            //Mapping many-to-many relation between task/employees and task/equipment

            base.OnModelCreating(modelBuilder);
        }















        /*










        public static void SetEmployeeProfileImage(string username, string imagePath)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                
                cnn.Execute("UPDATE Employees SET Image = '" + imagePath + "' WHERE Username = '" + username + "'");
            }
        }
        public static string GetEmployeeProfileImage(string username)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                
                dynamic temp = cnn.QuerySingle("SELECT * FROM Employees WHERE Username = '" + username + "'");
                string tempPath = temp.Image;
                if (tempPath == null)
                {
                    return "assets/images/avatars/profile.jpg";
                }
                return tempPath;
            }
        }
        public static void SetTaskImage(int ID, string imagePath)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                cnn.Execute("UPDATE Tasks SET Image = '" + imagePath + "' WHERE ID = '" + ID + "'");
            }
        }
        public static string GetTaskImage(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {

                dynamic temp = cnn.QuerySingle("SELECT * FROM Tasks WHERE ID = '" + ID + "'");
                string tempPath = temp.Image;
                if (tempPath == null)
                {
                    return "assets/images/tasks/placeholder.png";
                }
                return tempPath;
            }
        }
        public static void SetAccessToken(string token, int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                cnn.Execute("UPDATE Employees SET AccessToken = '" + token + "' WHERE ID = " + id.ToString());
            }
        }
        public static Employee GetAccessTokenUser(string token)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                dynamic output = cnn.QuerySingle<dynamic>("SELECT * FROM Employees WHERE AccessToken = '" + token + "'");
                Employee temp = new Employee(output.Firstname, output.Lastname, output.Type, (double)output.Wage,
                    new ContactInfo(output.Email, output.Phonenumber),
                    new Address(output.Address, output.ZIP, output.City, output.Note));
                temp.hashCode = output.Password;
                temp.userName = output.Username;
                
                temp.id = (int)output.ID;
                temp.accessToken = output.AccessToken;
                return temp;
            }
        }
        public static void DeleteElementFromDatabase<T>(string table, int id)
        {
            if (table == "Employee")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    cnn.Execute("DELETE FROM Employees WHERE ID = " + id.ToString());
                }
            }
            else if (table == "Task")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    cnn.Execute("DELETE FROM Tasks WHERE ID = " + id.ToString());
                }
            }
            else if (table == "Customer")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    cnn.Execute("DELETE FROM Customers WHERE ID = " + id.ToString());
                }
            }
            else if (table == "Equipment")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    cnn.Execute("DELETE FROM Equipments WHERE ID = " + id.ToString());
                }
            }
        }
        public static void EmployeeNoLongerEmployee(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                cnn.Execute("UPDATE Employees SET Employed = 'Unemployed' WHERE ID = " + id.ToString());
            }
        }

        public static void SaveElementToDatabase<T>(dynamic element)
        {

            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Employee employee = (Employee)element;
                    string sqlStatement = "INSERT INTO Employees (Wage, Firstname, Lastname, Type, Email, Phonenumber, Address, ZIP, City, Note, Username, Password, Employed) " +
                                          "VALUES ('" + employee.wage + "', '" + employee.firstName + "', '" + employee.lastName + "', '" + employee.type + "', '" + employee.contactInfo.email +
                                          "', '" + employee.contactInfo.phoneNumber + "', '" + employee.address.livingAddress + "', '" + employee.address.ZIP + "', '" + employee.address.city + "', '" + employee.address.note + 
                                          "', '" + employee.userName + "', '" + employee.hashCode + "', '" + employee.employed + "')";
                    cnn.Execute(sqlStatement);
                    RetrieveSpecificElementIDFromDatabase(employee);
                }
            }
            else if (element is Task)
            {

                if (element is Moving)
                {
                    using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                    {
                        Moving task = (Moving)element;
                        string sqlStatement = "INSERT INTO Tasks (Name, Type, Date, Duration, CustomerID, EmployeeIDs, EquipmentIDs, Income, Expenses, Balance, Workphone, DestinationAddress, DestinationCity, DestinationZIP, DestinationNote" +
                                              "StartingAddress, StartingCity, StartingZIP, StartingNote, Description) " +
                                              "VALUES ('" + task.name + "', '" + task.type + "', '" + string.Join("/", task.dates) + "', '" + task.taskDuration.ToString() + "', '" +
                                               task.assignedCustomer.id + "', '"  + task.employeesIdString + "', '" + task.equipmentsIdString + "', '" + task.income + "', '" + task.expenses + "', '" + task.balance + "', '" + task.workPhoneNumber + "', '" +
                                               task.destination.livingAdress + "', '" + task.destination.city + "', '" + task.destination.ZIP + "', '" + task.destination.note + "', '" +
                                               task.startingAddress.livingAdress + "', '" + task.startingAddress.city + "', '" + task.startingAddress.ZIP + "', '" + task.startingAddress.note + "', '" + task.description + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(task);
                    }
                }
                else
                {
                    using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                    {
                        Delivery task = (Delivery)element;
                        string sqlStatement = "INSERT INTO Tasks (Name, Type, Date, Duration, CustomerID, EmployeeIDs, EquipmentIDs, Income, Expenses, Balance, Workphone, DestinationAddress, DestinationCity, DestinationZIP, DestinationNote, " +
                                               "Material, Quantity, Description) " +
                                               "VALUES ('" + task.name + "', '" + task.type + "', '" + string.Join("/", task.dates) + "', '" + task.taskDuration.ToString() + "', '" +
                                               task.assignedCustomer.id + "', '" + task.employeesIdString + "', '" + task.equipmentsIdString + "', '" + task.income + "', '" + task.expenses + "', '" + task.balance + "', '" + task.workPhoneNumber + "', '" +
                                               task.destination.livingAdress + "', '" + task.destination.city + "', '" + task.destination.ZIP + "', '" + task.destination.note + "', '" +
                                               task.material + "', '" + task.quantity + "', '" + task.description + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(task);
                    }
                }
            }
            else if (element is Customer)
            {

                if (element is Business)
                {
                    using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                    {
                        Business customer = (Business)element;
                        string sqlStatement = "INSERT INTO Customers (Name, Type, Address, ZIP, City, Note, Phonenumber, Email, CVR) " +
                                          "Values ('" + customer.businessName + "', '" + customer.type + "', '" +  "', '" + customer.address.livingAddress + "', '" +
                                          customer.address.ZIP + "', '" + customer.address.city + "', '" + "', '" + customer.address.note + "', '" + customer.contactInfo.phoneNumber + "', '" + customer.contactInfo.email + "', '" +
                                          customer.CVR + "', '" + customer.lentBoxes + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(customer);
                    }
                }
                else if (element is Private)
                {
                    using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                    {
                        Private customer = (Private)element;
                        string sqlStatement = "INSERT INTO Customers (Firstname, Lastname, Type, Address, ZIP, City, Note, Phonenumber, Email) " +
                                              "Values ('" + customer.firstName + "', '" + customer.lastName + "', '" + customer.type + "', '" + customer.address.livingAdress + "', '" +
                                              customer.address.ZIP + "', '" + customer.address.city + "', '" + customer.address.note + "', '" + customer.contactInfo.phoneNumber + "', '" + customer.contactInfo.email + "', '" + customer.lentBoxes + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(customer);
                    }
                }
                else if (element is Public)
                {
                    using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                    {
                        Public customer = (Public)element;
                        string sqlStatement = "INSERT INTO Customers (Name, Type, Address, ZIP, City, Note, Phonenumber, Email, EAN) " +
                                              "Values ('" + customer.businessName +  "', '" + customer.type + "', '" + customer.address.livingAdress + "', '" +
                                              customer.address.ZIP + "', '" + customer.address.city + "', '" + customer.address.note + "', '" + customer.contactInfo.phoneNumber + "', '" + customer.contactInfo.email + "', '" + customer.EAN + "', '" + customer.lentBoxes + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(customer);
                    }
                }

            }
            else if (element is Equipment)
            {

                if (element is Vehicle)
                {
                    using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                    {
                        Vehicle equipment = (Vehicle)element;
                        string sqlStatement = "INSERT INTO Equipments (Name, Type, Model, Plates) " +
                                              "Values ('" + equipment.name + "', '" + equipment.type + "', '" +
                                              equipment.model + "', '" + equipment.regNum + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(equipment);
                    }
                }
                else
                {
                    using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                    {
                        Tool equipment = (Tool)element;
                        string sqlStatement = "INSERT INTO Equipments (Name, Type) " +
                                               "Values ('" + equipment.name + "', '" + equipment.type + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(equipment);
                    }
                }


            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }


        }

        private static void RetrieveSpecificElementIDFromDatabase(dynamic element)
        {

            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.QuerySingle<dynamic>("SELECT * FROM Employees WHERE ID = (SELECT MAX(ID) FROM Employees)");
                    ((Employee)element).id = (int)output.ID;
                }
            }
            else if (element is Task)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.QuerySingle<dynamic>("SELECT * FROM Tasks WHERE ID = (SELECT MAX(ID) FROM Tasks)");
                    ((Task)element).id = (int)output.ID;
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.QuerySingle<dynamic>("SELECT * FROM Customers WHERE ID = (SELECT MAX(ID) FROM Customers)");
                    ((Customer)element).id = (int)output.ID;
                }
            }
            else if (element is Equipment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.QuerySingle<dynamic>("SELECT * FROM Equipments WHERE ID = (SELECT MAX(ID) FROM Equipments)");
                    ((Equipment)element).id = (int)output.ID;
                }
            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }


        }
        //public static void LoadSpecificElementFromDatabase<T>(dynamic element) 
        //{
        //    if (element is Employee)
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string elementID = element.id.ToString();
        //            dynamic output = cnn.QuerySingle<Employee>("select * from Employees where ID = " + elementID);

        //        }
        //    }
        //    else if (element is Appointment)
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string elementID = element.id.ToString();
        //            dynamic output = cnn.QuerySingle<Appointment>("select * from Appointments where ID = " + elementID, new DynamicParameters());
        //        }
        //    }
        //    else if (element is Customer)
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string elementID = element.id;
        //            dynamic output = cnn.QuerySingle<Customer>("select * from Customers where ID = " + elementID, new DynamicParameters());
        //        }
        //    }
        //    else if (element is Equipment)
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string elementID = element.id;
        //            dynamic output = cnn.QuerySingle<Equipment>("select * from Equipments where ID = " + elementID, new DynamicParameters());
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("Can only remove objects from database that are in the database");
        //    }
        //}
        private static List<DateTime> CalculateDateFromDatabaseString(string date)
        {
            string[] tempDates = date.Split("/");
            List<DateTime> tempDateTimes = new List<DateTime>();
            foreach (string tempdate in tempDates)
            {
                tempDateTimes.Add(Convert.ToDateTime(tempdate));
                /*
                DateTime.TryParseExact(tempdate, "yy/M/d",  
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime dateFormat);
                tempDateTimes.Add(dateFormat);
                
            }
            
            return tempDateTimes;

        }
        public static dynamic LoadElementFromDatabase(string type, int id)
        {
            if (type == "Employee")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Employees) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }
                    dynamic output = cnn.QuerySingle<dynamic>("select * from Employees where ID = " + id.ToString());


                    Employee temp = new Employee(output.Firstname, output.Lastname, output.Type, (double)output.Wage,
                            new ContactInfo(output.Email, output.Phonenumber),
                            new Address(output.Address, output.ZIP, output.City, output.Note));
                    temp.hashCode = output.Password;
                    temp.userName = output.Username;

                    temp.id = (int)output.ID;

                    return temp;
                }
            }
            /*            else if (type == "Private")
                        {
                            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                            {
                                string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
                                if (cnn.Execute(sqlTest) == 1)
                                {
                                    return null;
                                }

                                dynamic output = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());

                                Private temp = new Private(output.Firstname, output.Lastname, output.Type,
                                    new Address(output.Address, output.ZIP, output.City, output.Note),
                                    new ContactInfo(output.Email, output.Phonenumber));

                                temp.id = (int)output.ID;
                                return temp;
                            }
                        }
            else if (type == "Customer")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    dynamic output = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());

                    Private temp = new Private(output.Firstname, output.Lastname, output.Type,
                        new Address(output.Address, output.ZIP, output.City, output.Note),
                        new ContactInfo(output.Email, output.Phonenumber));

                    temp.id = (int)output.ID;
                    temp.lentBoxes = (int)output.Lentboxes;
                    return temp;
                }
            }
            else if (type == "Business")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        Business temp = new Business(output.firstName, output.Lastname, output.Type,
    new Address(output.Address, output.ZIP, output.City, output.Note),
    new ContactInfo(output.Email, output.Phonenumber),
    output.Name, output.CVR);

                        temp.id = (int)output.ID;
                        return temp;
                    }
                    else if (output.Type == "Public")
                    {
                        Public temp = new Public(output.firstName, output.Lastname, output.Type,
    new Address(output.Address, output.ZIP, output.City, output.Note),
    new ContactInfo(output.Email, output.Phonenumber),
    output.Name, output.EAN);

                        temp.id = (int)output.ID;
                        return temp;
                    }
                    else if (output.Type == "Private")
                    {
                        Private temp = new Private(output.Firstname, output.Lastname, output.Type,
    new Address(output.Address, output.ZIP, output.City, output.Note),
    new ContactInfo(output.Email, output.Phonenumber));

                    temp.id = (int)output.ID;
                    temp.lentBoxes = (int)output.Lentboxes;
                    return temp;
                }
            }
            else if (type == "Public")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        throw new Exception("No customer with that type");
                    }

                    dynamic output = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());

                    Public temp = new Public(output.firstName, output.Lastname, output.Type,
                        new Address(output.Address, output.ZIP, output.City, output.Note),
                        new ContactInfo(output.Email, output.Phonenumber), 
                        output.Name, output.EAN);

                    temp.id = (int)output.ID;
                    temp.lentBoxes = (int)output.Lentboxes;
                    return temp;
                }
            }
            /*            else if (type == "Business")
                        {
                            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                            {
                                string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
                                if (cnn.Execute(sqlTest) == 1)
                                {
                                    return null;
                                }

                                dynamic output = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());

                                Business temp = new Business(output.firstName, output.Lastname, output.Type,
                                    new Address(output.Address, output.ZIP, output.City, output.Note),
                                    new ContactInfo(output.Email, output.Phonenumber),
                                    output.Name, output.CVR);

                                temp.id = (int)output.ID;
                                return temp;
                            }
                        }
                        else if (type == "Public")
                        {
                            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                            {
                                string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
                                if (cnn.Execute(sqlTest) == 1)
                                {
                                    return null;
                                }

                                dynamic output = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());

                                Public temp = new Public(output.firstName, output.Lastname, output.Type,
                                    new Address(output.Address, output.ZIP, output.City, output.Note),
                                    new ContactInfo(output.Email, output.Phonenumber),
                                    output.Name, output.EAN);

                                temp.id = (int)output.ID;
                                return temp;
                            }
                        }
            else if (type == "Vehicle")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Equipments) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    dynamic output = cnn.QuerySingle<dynamic>("select * from Equipments where ID = " + id.ToString());

                    Vehicle temp = new Vehicle(output.Name, output.Type, output.Model, output.Plates);

                    temp.id = (int)output.ID;
                    return temp;
                }
            }

            else if (type == "Tool")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Equipments) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    dynamic output = cnn.QuerySingle<dynamic>("select * from Equipments where ID = " + id.ToString());

                    Tool temp = new Tool(output.Name, output.Type);

                    temp.id = (int)output.ID;
                    return temp;
                }
            }
            else if (type == "Delivery")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Tasks) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    dynamic output = cnn.QuerySingle<dynamic>("select * from Tasks where ID = " + id.ToString());

                    Delivery temp = new Delivery(output.Name, output.Type, GetCustomerFromDatabaseID((int)output.ID),
                                new Address(output.DestinationAddress, output.DestinationZIP,
                                output.DestinationCity, output.DestinationNote),
                                (double)output.Income, CalculateDateFromDatabaseString(output.Date),
                                output.Note, output.Workphone, output.Material, (int)output.Quantity);
                    temp.id = (int)output.ID;
                    temp.taskDuration = ConvertDurationStringFromDatabaseToTimeSpan(output.Duration);
                    temp.equipmentsIdString = output.EquipmentIDs;
                    temp.assignedEmployees = (List<Employee>)GetElementsFromIDString("Employee", output.EmployeeIDs);
                    temp.assignedEquipment = (List<Equipment>)GetElementsFromIDString("Equipment", output.EquipmentIDs);
                    temp.employeesIdString = output.EmployeeIDs;
                    return temp;
                }
            }
            else if (type == "Moving")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Tasks) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    dynamic output = cnn.QuerySingle<dynamic>("select * from Tasks where ID = " + id.ToString());

                    Moving temp = new Moving(output.Name, output.Type, GetCustomerFromDatabaseID((int)output.ID),
                                new Address(output.DestinationAddress, output.DestinationZIP,
                                output.DestinationCity, output.DestinationNote), output.income, CalculateDateFromDatabaseString(output.Date), output.Note, output.Workphone,
                                new Address(output.StartingAddress, output.ZIP, output.City, output.Note), output.Lentboxes);
                    temp.id = (int)output.ID;
                    temp.image = GetTaskImage(temp.id);
                    temp.taskDuration = ConvertDurationStringFromDatabaseToTimeSpan(output.Duration);
                    temp.equipmentsIdString = output.EquipmentIDs;
                    temp.employeesIdString = output.EmployeeIDs;
                    temp.assignedEmployees = (List<Employee>)GetElementsFromIDString("Employee", output.EmployeeIDs);
                    temp.assignedEquipment = (List<Equipment>)GetElementsFromIDString("Equipment", output.EquipmentIDs);
                    return temp;
                }
            }
            else
            {
                return new Exception("Can't load non-existing object");
            }

        }

        public static dynamic LoadAllElementsFromDatabase(string type)
        {
            if (type == "Employee")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Employees) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    dynamic output = cnn.Query<dynamic>("select * from Employees");

                    List<Employee> tempList = new List<Employee>();
                    foreach (var put in output)
                    {
                        Employee temp;
                        tempList.Add((temp = new Employee(put.Firstname, put.Lastname, put.Type, (double) put.Wage,
                            new ContactInfo(put.Email, put.Phonenumber),
                            new Address(put.Address, put.ZIP, put.City, put.Note))));
                        temp.id = (int) put.ID;
                    }

                    return tempList;
                }
            }
            else if (type == "Customer")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    List<Customer> tempList = new List<Customer>();
                    dynamic output = cnn.Query<dynamic>("select * from Customers");
                    foreach (var put in output) 
                    {
                            if(put.Type == "Private")
                            {
                                Private temp;
                                tempList.Add(temp = new Private(put.Firstname, put.Lastname, put.Type,
                                    new Address(put.Address, put.ZIP, put.City, put.Note),
                                    new ContactInfo(put.Email, put.Phonenumber)));
                                temp.id = (int)put.ID;

                                if (put.Lentboxes != null)
                                {
                                    temp.lentBoxes = (int)put.Lentboxes;    
                                }
                                

                            }
                            else if (put.Type == "Business")
                            {
                                Business temp;
                                tempList.Add(temp = new Business(put.Firstname, put.Lastname, put.Type,
                                    new Address(put.Address, put.ZIP, put.City, put.Note),
                                    new ContactInfo(put.Email, put.Phonenumber),
                                    put.Name, put.CVR));
                                temp.id = (int)put.ID;
                                if (put.Lentboxes != null)
                                {
                                    temp.lentBoxes = (int)put.Lentboxes;    
                                }

                            }
                            else if (put.Type == "Public")
                            {
                                Public temp;
                                tempList.Add(temp = new Public(put.Firstname, put.Lastname, put.Type,
                                    new Address(put.Address, put.ZIP, put.City, put.Note),
                                    new ContactInfo(put.Email, put.Phonenumber),
                                    put.Name, put.EAN));
                                temp.id = (int)put.ID;
                                if (put.Lentboxes != null)
                                {
                                    temp.lentBoxes = (int)put.Lentboxes;    
                                }

                            }
                            else
                            {
                                throw new Exception("No customer with that type");
                            }
                    }
                    return tempList;

                }
            }
            else if (type == "Equipment")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest =
                        "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Equipments) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    List<Equipment> tempList = new List<Equipment>();
                    dynamic output = cnn.Query<dynamic>("select * from Equipments");

                    foreach (var put in output)
                    {
                        if (put.Type == "Tool")
                        {
                            Tool temp;
                            tempList.Add(temp = new Tool(output.Name, output.Type));
                            temp.id = (int)output.ID;
                        }
                        else if (put.Type == "Vehicle")
                        {
                            Vehicle temp;
                            tempList.Add(temp = new Vehicle(put.Name, put.Type, put.Model, put.Plates));
                            temp.id = (int)put.ID;
                        }
                        else
                        {
                            throw new Exception("No Equipment with that type");
                        }
                    }
                    return tempList;
                }
            }
            else if (type == "Task")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Tasks) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }

                    dynamic output = cnn.Query<dynamic>("select * from Tasks");
                    List<Task> tempList = new List<Task>();


                    foreach (var put in output)
                    {
                        if (put.Type == "Delivery")
                        {
                            Delivery temp;
                            tempList.Add(temp = new Delivery(put.Name, put.Type,
                                GetCustomerFromDatabaseID((int)put.CustomerID),
                                new Address(put.DestinationAddress, put.DestinationZIP,
                                    put.DestinationCity, put.DestinationNote),
                                (double)put.Income, CalculateDateFromDatabaseString(put.Date),
                                put.Description, put.Workphone, put.Material, (int)put.Quantity));
                            temp.id = (int)put.ID;
                            temp.taskDuration = ConvertDurationStringFromDatabaseToTimeSpan(put.Duration);
                            temp.image = GetTaskImage(temp.id);
                            temp.equipmentsIdString = put.EquipmentIDs;
                            temp.employeesIdString = put.EmployeeIDs;
                        }
                        else if (put.Type == "Moving")
                        {
                            Moving temp;

                            tempList.Add(temp = new Moving(put.Name, put.Type,
                                GetCustomerFromDatabaseID((int) put.CustomerID),
                                new Address(put.DestinationAddress, put.DestinationZIP,
                                    put.DestinationCity, put.DestinationNote), put.income,
                                CalculateDateFromDatabaseString(put.Date), put.Description, put.Workphone,
                                new Address(put.StartingAddress, put.StartingZIP, put.StartingCity, put.StartingNote), put.Lentboxes));
                            temp.id = (int)put.ID;
                            temp.image = GetTaskImage(temp.id);
                            temp.taskDuration = ConvertDurationStringFromDatabaseToTimeSpan(put.Duration);
                            temp.equipmentsIdString = put.EquipmentIDs;
                            temp.employeesIdString = put.EmployeeIDs;
                        }

                    }

                    return tempList;
                }
                
            }
            else if (type == "Furniture")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Furnitures) THEN 1 ELSE 0 END) AS isEmpty";
                    if (cnn.Execute(sqlTest) == 1)
                    {
                        return null;
                    }
                    List<Furniture> tempList = new List<Furniture>();
                    dynamic output = cnn.Query<dynamic>("select * from Furnitures");
                    Furniture temp;
                    foreach (var put in output)
                    {
                        tempList.Add(temp = new Furniture(put.Name, (double)put.CubicSize, put.Type, (double)put.Weight));
                        temp.id = (int)put.ID;
                    }
                    return tempList;
                }
            }
            else
            {
                return new Exception("Can't load non-existing object");
            }
        }



        public static TimeSpan ConvertDurationStringFromDatabaseToTimeSpan(string duration)
        {
            string[] split = duration.Split(":");
            return new TimeSpan(Convert.ToInt32(split[2]), Convert.ToInt32(split[1]), Convert.ToInt32(split[0]));
        }
        //public static dynamic LoadAllOfSpecificElementFromDatabase(string element)
        //{

        //    int incr = 0;

        //    if (element == "Employee")
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Employees) THEN 1 ELSE 0 END) AS isEmpty";
        //            if (cnn.Execute(sqlTest) == 1)
        //            {
        //                return null;
        //            }
        //        }
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            var output = cnn.Query<dynamic>("select * from Employees", new DynamicParameters());

        //            List<Employee> temp = new List<Employee>();
        //            foreach (var employee in output)
        //            {
        //                temp.Add(new Employee(output.ElementAt(incr).Firstname, output.ElementAt(incr).Lastname, (double)output.ElementAt(incr).Wage,
        //                    new ContactInfo(output.ElementAt(incr).Email, output.ElementAt(incr).Phonenumber),
        //                    new Address(output.ElementAt(incr).Address, output.ElementAt(incr).ZIP, output.ElementAt(incr).City, output.ElementAt(incr).Note)));
        //                temp.ElementAt(incr).id = (int)output.ElementAt(incr).ID;
        //                temp.ElementAt(incr).appointmentIdString = output.ElementAt(incr).AppointmentIDs;
        //                incr++;
        //            }
        //            return temp;
        //        }
        //    }
        //    else if (element == "Appointment")
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Appointments) THEN 1 ELSE 0 END) AS isEmpty";
        //            if (cnn.Execute(sqlTest) == 1)
        //            {
        //                return null;
        //            }
        //        }
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            var output = cnn.Query<dynamic>("select * from Appointments", new DynamicParameters());
        //            List<Appointment> temp = new List<Appointment>();
        //            incr = 0;
        //            foreach (var appointment in output)
        //            {
        //                if (output.ElementAt(incr).Type == "Delivery")
        //                {
        //                    temp.Add(new Delivery(output.ElementAt(incr).Name, output.ElementAt(incr).Type, (double)output.ElementAt(incr).Duration, GetCustomerFromDatabaseID((int)output.ElementAt(incr).ID),
        //                        new Address(output.ElementAt(incr).DestinationAddress, output.ElementAt(incr).DestinationZIP,
        //                        output.ElementAt(incr).DestinationCity, output.ElementAt(incr).DestinationNote),
        //                        (double)output.ElementAt(incr).Income, CalculateDateFromDatabaseString(output.ElementAt(incr).Date),
        //                        output.ElementAt(incr).Note, output.ElementAt(incr).Workphone, output.ElementAt(incr).Material, (int)output.ElementAt(incr).Quantity));
        //                    temp.ElementAt(incr).id = (int)output.ElementAt(incr).ID;
        //                    temp.ElementAt(incr).equipmentsIdString = output.ElementAt(incr).EquipmentIDs;
        //                    temp.ElementAt(incr).employeesIdString = output.ElementAt(incr).EmployeeIDs;
        //                }
        //                else
        //                {
        //                    temp.Add(new Moving(output.ElementAt(incr).Name, output.ElementAt(incr).Type, (double)output.ElementAt(incr).Duration, GetCustomerFromDatabaseID((int)output.ElementAt(incr).ID),
        //                        new Address(output.ElementAt(incr).DestinationAddress, output.ElementAt(incr).DestinationZIP, output.ElementAt(incr).DestinationCity, output.ElementAt(incr).DestinationNote),
        //                        (double)output.ElementAt(incr).Income, CalculateDateFromDatabaseString(output.ElementAt(incr).Date),
        //                        output.ElementAt(incr).Note, output.ElementAt(incr).Workphone,
        //                        new Address(output.ElementAt(incr).StartingAddress, output.ElementAt(incr).StartingZIP, output.ElementAt(incr).StartingCity, output.ElementAt(incr).StartingNote),
        //                        (int)output.ElementAt(incr).Lentboxes));
        //                    temp.ElementAt(incr).id = (int)output.ElementAt(incr).ID;
        //                    temp.ElementAt(incr).equipmentsIdString = output.ElementAt(incr).EquipmentIDs;
        //                    temp.ElementAt(incr).employeesIdString = output.ElementAt(incr).EmployeeIDs;
        //                }
        //                incr++;
        //            }
        //            return temp;
        //        }

        //    }
        //    else if (element is "Customer")
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Customers) THEN 1 ELSE 0 END) AS isEmpty";
        //            if (cnn.Execute(sqlTest) == 1)
        //            {
        //                return null;
        //            }
        //        }
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            var output = cnn.Query<dynamic>("select * from Customers", new DynamicParameters());
        //            List<Customer> temp = new List<Customer>();
        //            incr = 0;
        //            foreach (var customer in output)
        //            {
        //                if (output.ElementAt(incr).Type == "Business")
        //                {
        //                    temp.Add(new Business(output.ElementAt(incr).Firstname, output.ElementAt(incr).Lastname, output.ElementAt(incr).Type,
        //                        new Address(output.ElementAt(incr).Address, output.ElementAt(incr).ZIP, output.ElementAt(incr).City, output.ElementAt(incr).Note),
        //                        new ContactInfo(output.ElementAt(incr).Email, output.ElementAt(incr).Phonenumber), output.ElementAt(incr).EAN, output.ElementAt(incr).CVR));
        //                    temp.ElementAt(incr).id = (int)output.ElementAt(incr).ID;
        //                }
        //                else
        //                {
        //                    temp.Add(new PrivateCustomer(output.ElementAt(incr).Firstname, output.ElementAt(incr).Lastname, output.ElementAt(incr).Type,
        //                        new Address(output.ElementAt(incr).Address, output.ElementAt(incr).ZIP, output.ElementAt(incr).City, output.ElementAt(incr).Note),
        //                        new ContactInfo(output.ElementAt(incr).Email, output.ElementAt(incr).Phonenumber)));
        //                    temp.ElementAt(incr).id = (int)output.ElementAt(incr).ID;
        //                }
        //                incr++;
        //            }
        //            return temp;
        //        }


        //    }
        //    else if (element is "Equipment")
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            string sqlTest = "SELECT (CASE WHEN NOT EXISTS(SELECT NULL FROM Equipments) THEN 1 ELSE 0 END) AS isEmpty";
        //            if (cnn.Execute(sqlTest) == 1)
        //            {
        //                return null;
        //            }
        //        }
        //        using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
        //        {
        //            var output = cnn.Query<dynamic>("select * from Equipments", new DynamicParameters());
        //            List<Equipment> temp = new List<Equipment>();
        //            incr = 0;
        //            foreach (var equipment in output)
        //            {
        //                if (output.ElementAt(incr).Type == "Vehicle")
        //                {
        //                    temp.Add(new Vehicle(output.ElementAt(incr).Name, output.ElementAt(incr).Type, output.ElementAt(incr).Model, output.ElementAt(incr).Plates));
        //                    temp.ElementAt(incr).id = (int)output.ElementAt(incr).ID;
        //                    temp.ElementAt(incr).appointmentIdString = output.ElementAt(incr).AppointmentIDs;
        //                }
        //                else
        //                {
        //                    temp.Add(new Gear(output.ElementAt(incr).Name, output.ElementAt(incr).Type));
        //                    temp.ElementAt(incr).id = (int)output.ElementAt(incr).ID;
        //                    temp.ElementAt(incr).appointmentIdString = output.ElementAt(incr).AppointmentIDs;
        //                }
        //                incr++;
        //            }
        //            return temp;
        //        }


        //    }
        //    else
        //    {
        //        throw new Exception("Can only remove objects from database that are in the database");
        //    }
        //}
        /*
        public static Customer GetCustomerFromDatabaseID(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {

                dynamic output = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());
                string outputType = output.Type;
                if (outputType == "Private")
                {
                    Private temp = new Private(output.Firstname, output.Lastname, output.Type,
                        new Address(output.Address, output.ZIP, output.City, output.Note),
                        new ContactInfo(output.Email, output.Phonenumber));
                    temp.id = (int)output.ID;
                    if (output.Lentboxes != null)
                    {
                        temp.lentBoxes = (int)output.Lentboxes;    
                    }
                    return temp;
                }
                else if (outputType == "Business")
                {
                    dynamic output_two = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());

                    Business temp = new Business(output_two.Firstname, output_two.Lastname, output_two.Type,
                        new Address(output_two.Address, output_two.ZIP, output_two.City, output_two.Note),
                        new ContactInfo(output_two.Email, output_two.Phonenumber),
                        output_two.EAN, output_two.CVR);

                    temp.id = (int)output_two.ID;
                    if (output.Lentboxes != null)
                    {
                        temp.lentBoxes = (int)output.Lentboxes;    
                    }

                    return temp;
                }
                else if (outputType == "Public")
                {
                    dynamic output_two = cnn.QuerySingle<dynamic>("select * from Customers where ID = " + id.ToString());

                    Public temp = new Public(output_two.firstName, output_two.Lastname, output_two.Type,
                        new Address(output_two.Address, output_two.ZIP, output_two.City, output_two.Note),
                        new ContactInfo(output_two.Email, output_two.Phonenumber),
                        output_two.Name, output_two.EAN);

                    temp.id = (int)output_two.ID;
                    if (output.Lentboxes != null)
                    {
                        temp.lentBoxes = (int)output.Lentboxes;    
                    }
                    return temp;
                }
                else throw new Exception();
            }
        }
        public static dynamic GetElementsFromIDString(string type, string id)
        {
            if (type == "Employee")
            {
                Employee temp;
                List<Employee> tempList = new List<Employee>();
                string[] tempIDs = id.Split("/");
                List<string> eachID = tempIDs.ToList();
                eachID.RemoveAt(eachID.Count - 1);
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    foreach (string each in eachID)
                    {
                        dynamic output = cnn.QuerySingle<dynamic>("select * from Employees where ID = " + each);
                        temp = new Employee(output.Firstname, output.Lastname, output.Type, output.Wage,
                            new ContactInfo(output.Email, output.Phonenumber),
                            new Address(output.Address, output.ZIP, output.City, output.Note));
                        temp.hashCode = output.Password;
                        temp.userName = output.Username;
                        temp.employed = output.Employed;
                        temp.id = (int)output.ID;
                        tempList.Add(temp);

                    }
                    return tempList;
                }
            }
            else if (type == "Equipment")
            {
                Equipment temp;
                List<Equipment> tempList = new List<Equipment>();
                string[] tempIDs = id.Split("/");
                List<string> eachID = tempIDs.ToList();
                eachID.RemoveAt(eachID.Count - 1);
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    foreach (string each in eachID)
                    {
                        dynamic output = cnn.QuerySingle<dynamic>("select * from Equipments where ID = " + each);
                        if (output.Type == "Vehicle")
                        {
                            temp = new Vehicle(output.Name, output.Type, output.Model, output.Plates);
                            temp.id = (int)output.ID;
                            tempList.Add(temp);
                        }
                        else
                        {
                            temp = new Tool(output.Name, output.Type);
                            temp.id = (int)output.ID;
                            tempList.Add(temp);
                        }
                    }
                    return tempList;
                }
            }
            else
            {
                throw new Exception("Unknown element");
            }
        }
        
        public static List<Employee> GetEmployeeFromEmployeeString(string id)
        {
            Employee temp;
            List<Employee> tempList = new List<Employee>();
            string[] tempIDs = id.Split("/");
            List<string> eachID = tempIDs.ToList();
            eachID.RemoveAt(eachID.Count-1);
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                foreach (string each in eachID)
                {
                    dynamic output = cnn.QuerySingle<dynamic>("select * from Employees where ID = " + each);
                    temp = new Employee(output.Firstname, output.Lastname, output.Type, output.Wage,
                        new ContactInfo(output.Email, output.Phonenumber),
                        new Address(output.Address, output.ZIP, output.City, output.Note));
                    temp.hashCode = output.Password;
                    temp.userName = output.Username;
                    temp.id = (int)output.ID;
                    tempList.Add(temp);

                }
                return tempList;
            }
        }

        private static List<Equipment> GetEquipmentFromEquipmentString(string id)
        {
            Equipment temp;
            List<Equipment> tempList = new List<Equipment>();
            string[] tempIDs = id.Split("/");
            List<string> eachID = tempIDs.ToList();
            eachID.RemoveAt(eachID.Count-1);
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                foreach (string each in eachID)
                {
                    dynamic output = cnn.QuerySingle<dynamic>("select * from Equipments where ID = " + each);
                    if (output.Type == "Vehicle")
                        {
                            temp = new Vehicle(output.Name, output.Type, output.Model, output.Plates);
                        temp.id = (int)output.ID;
                        tempList.Add(temp);
                        }
                    else
                        {
                            temp = new Tool(output.Name, output.Type);
                        temp.id = (int)output.ID;
                        tempList.Add(temp);
                        }
                }
                return tempList;
            }
        }
        

        public static void UpdateElementInDatabase<T>(dynamic element)
        {

            if (element is Employee)
            {
                UpdateEmployee((Employee)element);
            }
            else if (element is Task)
            {
                UpdateTask((Task)element);
            }
            else if (element is Customer)
            {
                UpdateCustomer((Customer)element);
            }
            else if (element is Equipment)
            {
                UpdateEquipment((Equipment)element);
            }
        }
        private static void UpdateEmployee(Employee employee)
        {
            string sqlStatement = null;
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                sqlStatement = "update Employees " +
                               "set Wage = '" + employee.wage +
                               "', Firstname = '" + employee.firstName +
                               "', Lastname = '" + employee.lastName +
                               "', Type = '" + employee.type +
                               "', Email = '" + employee.contactInfo.email +
                               "', Note = '" + employee.address.note + 
                               "', Phonenumber = '" + employee.contactInfo.phoneNumber +
                               "', Address = '" + employee.address.livingAddress +
                               "', ZIP = '" + employee.address.ZIP +
                               "', City = '" + employee.address.city + "' where " +
                               "ID = " + employee.id;
                cnn.Execute(sqlStatement);
            }
        }
        private static void UpdateEquipment(Equipment equipment)
        {
            string sqlStatement = null;
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                sqlStatement = "update Equipments " +
                               "set Name = '" + equipment.name +
                               "', Type = '" + equipment.type + "' where " +
                               "ID = " + equipment.id;
                cnn.Execute(sqlStatement);
                if (equipment is Vehicle)
                {
                    sqlStatement = "update Equipments " +
                                   "set Model = '" + ((Vehicle)equipment).model +
                                   "', Plates = '" + ((Vehicle)equipment).regNum + "' where " +
                                   "ID = " + equipment.id;
                    cnn.Execute(sqlStatement);
                }
            }
        }
        private static void UpdateTask(Task task)
        {
            string sqlStatement = null;
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                sqlStatement = "update Tasks " +
                               "set Name = '" + task.name +
                               "', Type = '" + task.type +
                               "', Duration = '" + task.taskDuration +
                               "', EmployeeIDs = '" + task.employeesIdString +
                               "', EquipmentIDs = '" + task.equipmentsIdString +
                               "', CustomerID = '" + task.assignedCustomer.id +
                               "', DestinationAddress = '" + task.destination.livingAddress +
                               "', DestinationCity = '" + task.destination.city +
                               "', DestinationZIP = '" + task.destination.ZIP +
                               "', DestinationNote = '" + task.destination.note +
                               "', Income = '" + task.income +
                               "', Expenses = '" + task.expenses +
                               "', Balance = '" + task.balance +
                               "', Date = '" + string.Join("/", task.dates) +
                               "', Workphone = '" + task.workPhoneNumber +
                               "', Description = '" + task.description +
                               "' where " +
                               "ID = " + task.id;
                cnn.Execute(sqlStatement);
                if (task is Moving)
                {
                    sqlStatement = "update Tasks " +
                                   "set StartingAddress = '" + ((Moving)task).startingAddress.livingAddress +
                                   "', StartingCity = '" + ((Moving)task).startingAddress.city +
                                   "', StartingZIP = '" + ((Moving)task).startingAddress.ZIP +
                                   "', StartingNote = '" + ((Moving)task).startingAddress.note +
                                   "', LentBoxes = '" + ((Moving)task).lentBoxes + "' where " +
                                    "ID = " + task.id;
                    cnn.Execute(sqlStatement);
                }
                if (task is Delivery)
                {
                    sqlStatement = "update Tasks " +
                                   "set Material = '" + ((Delivery)task).material +
                                   "', Quantity = '" + ((Delivery)task).quantity + "' where " +
                                    "ID = " + task.id;
                    cnn.Execute(sqlStatement);
                }
            }
        }

        private static void UpdateCustomer(Customer customer)
        {
            string sqlStatement = null;
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                sqlStatement = "update Customers " +
                               "set Type = '" + customer.type +
                               "', Address = '" + customer.address.livingAddress +
                               "', ZIP = '" + customer.address.ZIP +
                               "', City = '" + customer.address.city +
                               "', Note = '" + customer.address.note +
                               "', Email = '" + customer.contactInfo.email +
                               "', Phonenumber = '" + customer.contactInfo.phoneNumber + "' where " +
                               "ID = " + customer.id;
                cnn.Execute(sqlStatement);
                if (customer is Business)
                {
                    sqlStatement = "update Customers " +
                                   "set CVR = '" + ((Business)customer).CVR +
                                   "', Name = '" + ((Business)customer).businessName + "' where " +
                               "ID = " + customer.id;
                    cnn.Execute(sqlStatement);
                }
                else if (customer is Private)
                {
                    sqlStatement = "update Customers " +
                                    "set Firstname = '" + ((Private)customer).firstName +
                                    "', Lastname = '" + ((Private)customer).lastName + "' where " +
                                    "ID = " + customer.id;
                    cnn.Execute(sqlStatement);
                }
                else if (customer is Public)
                {
                    sqlStatement = "update Customers " +
                                    "set Name = '" + ((Public)customer).businessName +
                                    "', EAN = '" + ((Public)customer).EAN + "' where " +
                                    "ID = " + customer.id;
                    cnn.Execute(sqlStatement);
                }
            }

        }
        public static string LoadEmployeeHashPassword(string username)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                
                dynamic output = cnn.QuerySingle<dynamic>("select * from Employees WHERE Username = '" + username + "'");
                return output.Password;
            }
        }
        public static Employee LoadEmployeeFromHashPassword(string password)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {

                dynamic output = cnn.QuerySingle<dynamic>("select * from Employees WHERE Password = '" + password + "'");
                Employee temp = new Employee(output.Firstname, output.Lastname, output.Type, output.Wage,
                    new ContactInfo(output.Email, output.Phonenumber),
                    new Address(output.Address, output.ZIP, output.City, output.Note));
                temp.hashCode = output.Password;
                temp.userName = output.Username;
                temp.profilePhoto = output.Image;
                temp.id = (int)output.ID;
                return temp;
            }
        }
        public static Employee VerifyPassword(string password, string username)
        {

            string savedPasswordHash = LoadEmployeeHashPassword(username);
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
            Employee temp = LoadEmployeeFromHashPassword(savedPasswordHash);
            return temp;
        }
        private static string GetDefaultConnectionString()
        {

            return Startup.ConnectionString;
        }



  */
    }

}

