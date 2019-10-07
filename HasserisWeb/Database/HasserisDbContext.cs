using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.EntityFrameworkCore;


namespace HasserisWeb
{
    public class HasserisDbContext : DbContext
    {
        public static void DeleteElementFromDatabase<T>(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Employee employee = (Employee)element;
                    cnn.Execute("DELETE FROM Employees WHERE ID = (@id)", employee);
                }
            }
            else if (element is Appointment) 
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Appointment appointment = (Appointment)element;
                    cnn.Execute("DELETE FROM Appointments WHERE ID = (@id)", appointment);
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Customer customer = (Customer)element;
                    cnn.Execute("DELETE FROM Customers WHERE ID = (@id)", customer);
                }
            }
            else if (element is Equipment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Equipment equipment = (Equipment)element;
                    cnn.Execute("DELETE FROM Equipments WHERE ID = (@id)", equipment);
                }
            }
        }
        public static dynamic SaveElementToDatabase<T>(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Employee employee = (Employee)element;
                    cnn.Execute("INSERT INTO Employees (Wage, Firstname, Lastname, Email, Phonenumber, Address, ZIP, City) " +
                                "Values (@wage, @firstName, @lastName, @contactInfo.email, @contactInfo.phoneNumber, @address.livingAddress, @address.ZIP, @address.City)", employee);
                    return RetrieveSpecificElementIDFromDatabase(employee);
                }
            }
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    if (element is Moving)
                    {
                        Moving appointment = (Moving)element;
                        cnn.Execute("INSERT INTO Appointments (Name, Type, Date, Duration, CustomerID, Income, Expenses, Balance, Workphone, DestinationAddress, DestinationCity, DestinationZIP, " +
                                    "StartingAddress, StartingCity, StartingZIP, Lentboxes) " +
                                    "Values (@name, @type, @date, @duration, @customerID, @income, @expenses, @balance, @workPhoneNumber, @destination.livingAddress, @destination.City, @destination.ZIP" +
                                    "@startingAddress.livingAddress, @startingAddress.City, @startingAddress.ZIP)", appointment);
                        return RetrieveSpecificElementIDFromDatabase(appointment);
                    }
                    else
                    {
                        
                        Delivery appointment = (Delivery)element;
                        string[] destinationProperties = { appointment.destination.livingAdress, appointment.destination.city, appointment.destination.ZIP };
                        string dProperties = string.Join("", destinationProperties);
                        cnn.Execute("INSERT INTO Appointments (Name, Type, Date, Duration, CustomerID, Income, Expenses, Balance, Workphone, DestinationAddress, DestinationCity, DestinationZIP, " +
                                    "Material, Quantity) " +
                                    "VALUES (@name, @type, @date, @duration, @customerID, @income, @expenses, @balance, @workPhoneNumber, " + dProperties +  ", " + 
                                    "@material, @quantity)", appointment);
                        return RetrieveSpecificElementIDFromDatabase(appointment); 
                        
                    }

                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    if (element is Business)
                    {
                        Business customer = (Business)element;
                        cnn.Execute("INSERT INTO Customers (Firstname, Lastname, Type, Address, ZIP, City, Phonenumber, Email, CVR, EAN)" +
                                    " Values (@firstName, @lastName, @type, @address.livingAddress, @address.ZIP, @address.City, @contactInfo.phoneNumber, @contactInfo.Email" +
                                    " @CVR, @EAN)", customer);
                        return RetrieveSpecificElementIDFromDatabase(customer);
                    }
                    else
                    {
                        PrivateCustomer customer = (PrivateCustomer)element;
                        cnn.Execute("INSERT INTO Customers (Firstname, Lastname, Type, Address, ZIP, City, Phonenumber, Email)" +
                                    " Values (@firstName, @lastName, @type, @address.livingAddress, @address.ZIP, @address.City, @contactInfo.phoneNumber, @contactInfo.Email", customer);
                        return RetrieveSpecificElementIDFromDatabase(customer);
                    }
                }
            }
            else if (element is Equipment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    if (element is Vehicle)
                    {
                        Vehicle equipment = (Vehicle)element;
                        cnn.Execute("INSERT INTO Equipments (Name, Type, Model, Plates) Values (@name, @type, @model, @regNum )", equipment);
                        return RetrieveSpecificElementIDFromDatabase(equipment);
                    }
                    else
                    {
                        Gear equipment = (Gear)element;
                        cnn.Execute("INSERT INTO Equipments (Name, Type) Values (@name, @type)", equipment);
                        return RetrieveSpecificElementIDFromDatabase(equipment);
                    }

                }
            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }
        }
        private static dynamic RetrieveSpecificElementIDFromDatabase(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.Query<Employee>("SELECT TOP 1 * FROM Employees ORDER BY ID DESC", new DynamicParameters());
                    return output;
                }
            }
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.Query<Appointment>("SELECT TOP 1 * FROM Employees ORDER BY ID DESC", new DynamicParameters());
                    return output;
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.Query<Customer>("SELECT TOP 1 * FROM Customers ORDER BY ID DESC", new DynamicParameters());
                    return output;
                }
            }
            else if (element is Equipment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.Query<Equipment>("SELECT TOP 1 * FROM Equipments ORDER BY ID DESC", new DynamicParameters());
                    return output;
                }
            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }
        }
        public static dynamic LoadSpecificElementFromDatabase<T>(dynamic element) 
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string elementID = element.id;
                    dynamic output = cnn.Query<Employee>("select * from Employees where ID = " + elementID, new DynamicParameters());
                    return output;
                }
            }
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string elementID = element.id;
                    dynamic output = cnn.Query<Appointment>("select * from Appointments where ID = " + elementID, new DynamicParameters());
                    return output;
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string elementID = element.id;
                    dynamic output = cnn.Query<Customer>("select * from Customers where ID = " + elementID, new DynamicParameters());
                    return output;
                }
            }
            else if (element is Equipment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    string elementID = element.id;
                    dynamic output = cnn.Query<Equipment>("select * from Equipments where ID = " + elementID, new DynamicParameters());
                    return output;
                }
            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }
        }
        public static dynamic LoadAllOfSpecificElementFromDatabase<T>(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Employee>("select * from Employees", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Appointment>("select * from Appointments", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Customer>("select * from Customers", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else if (element is Equipment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Equipment>("select * from Equipments", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }
        }
        public static void ModifySpecificElementInDatabase<T>(dynamic element)
        {

            if (element is Employee)
            {
                UpdateEmployee((Employee)element);
            }
            else if (element is Appointment)
            {
                UpdateAppointment((Appointment)element);
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
                               "set Wage = " + employee.wage +
                               ", Firstname = " + employee.firstName +
                               ", Lastname = " + employee.lastName +
                               ", Email = " + employee.contactInfo.email +
                               ", Phonenumber = " + employee.contactInfo.phoneNumber +
                               ", Address = " + employee.address.livingAdress +
                               ", ZIP = " + employee.address.ZIP +
                               ", City = " + employee.address.city +
                               ", AppointmentIDs = " + employee.appointmentIdString;
                cnn.Execute(sqlStatement);
            }
        }
        private static void UpdateEquipment(Equipment equipment)
        {
            string sqlStatement = null;
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                sqlStatement = "update Equipments " +
                               "set AppointmentIDs = " + equipment.appointmentIdString +
                               ", Name = " + equipment.name +
                               ", Type = " + equipment.type;
                cnn.Execute(sqlStatement);
                if (equipment is Vehicle)
                {
                    sqlStatement = "update Equipments " +
                                   "set Model = " + ((Vehicle)equipment).model +
                                   ", Plates = " + ((Vehicle)equipment).regNum;
                    cnn.Execute(sqlStatement);
                }
            }
        }
        private static void UpdateAppointment(Appointment appointment)
        {
            string sqlStatement = null;
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                sqlStatement = "update Appointments " +
                               "set Name = " + appointment.name +
                               ", Type = " + appointment.type +
                               ", EmployeeIDs = " + appointment.employeesIdString +
                               ", EquipmentIDs = " + appointment.equipmentsIdString +
                               ", CustomerID = " + appointment.assignedCustomer.id +
                               ", DestinationAddress = " + appointment.destination.livingAdress +
                               ", DestinationCity = " + appointment.destination.city +
                               ", DestinationZIP = " + appointment.destination.ZIP +
                               ", Income = " + appointment.income +
                               ", Expenses = " + appointment.expenses +
                               ", Balance = " + appointment.balance +
                               ", Date = " + appointment.date.ToString() +
                               ", Workphone = " + appointment.workPhoneNumber;
                cnn.Execute(sqlStatement);
                if (appointment is Moving)
                {
                    sqlStatement = "update Appointments " +
                                   "set StartingAddress = " + ((Moving)appointment).startingAddress.livingAdress +
                                   ", StartingCity = " + ((Moving)appointment).startingAddress.city +
                                   ", StartingZIP = " + ((Moving)appointment).startingAddress.ZIP +
                                   ", LentBoxes = " + ((Moving)appointment).lentBoxes;
                    cnn.Execute(sqlStatement);
                }
                if (appointment is Delivery)
                {
                    sqlStatement = "update Appointments " +
                                   "set Material = " + ((Delivery)appointment).material +
                                   ", Quantity = " + ((Delivery)appointment).quantity;
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
                               "set Firstname = " + customer.firstName +
                               ", Lastname = " + customer.lastName +
                               ", Type = " + customer.type +
                               ", Address = " + customer.address.livingAdress +
                               ", ZIP = " + customer.address.ZIP +
                               ", City = " + customer.address.city +
                               ", Email = " + customer.contactInfo.email +
                               ", Phonenumber = " + customer.contactInfo.phoneNumber;
                cnn.Execute(sqlStatement);
                if (customer is Business)
                {
                    sqlStatement = "update Customers " +
                                   "set EAN = " + ((Business)customer).EAN +
                                   ", CVR = " + ((Business)customer).CVR;
                    cnn.Execute(sqlStatement);
                }
            }

        }
        private static string GetDefaultConnectionString()
        {

            return Startup.ConnectionString;
        }

    }
}
