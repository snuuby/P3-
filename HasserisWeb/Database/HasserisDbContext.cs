using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Diagnostics;
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
        public static void SaveElementToDatabase<T>(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Employee employee = (Employee)element;
                    string sqlStatement = "INSERT INTO Employees (Wage, Firstname, Lastname, Email, Phonenumber, Address, ZIP, City) " +
                                          "VALUES ('" + employee.wage + "', '" + employee.firstName + "', '" + employee.lastName + "', '" + employee.contactInfo.email +
                                          "', '" + employee.contactInfo.phoneNumber + "', '" + employee.address.livingAdress + "', '" + employee.address.ZIP + "', '" + employee.address.city + "')";
                    cnn.Execute(sqlStatement);
                    RetrieveSpecificElementIDFromDatabase(employee);
                }   
            }
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    if (element is Moving)
                    {
                        Moving appointment = (Moving)element;
                        string sqlStatement = "INSERT INTO Appointments (Name, Type, Date, Duration, CustomerID, Income, Expenses, Balance, Workphone, DestinationAddress, DestinationCity, DestinationZIP, DestinationNote" +
                                              "StartingAddress, StartingCity, StartingZIP, " +
                                              "Values ('" + appointment.name + "', '" + appointment.type + "', '" + appointment.date + "', '" + appointment.duration + "', '" +
                                               appointment.assignedCustomer.id + "', '" + appointment.income + "', '" + appointment.expenses + "', '" + appointment.balance + "', '" + appointment.workPhoneNumber + "', '" +
                                               appointment.destination.livingAdress + "', '" + appointment.destination.city + "', '" + appointment.destination.ZIP + "', '" + appointment.destination.note + "', '" + 
                                               appointment.startingAddress.livingAdress + "', '" + appointment.startingAddress.city + "', '" + appointment.startingAddress.ZIP + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(appointment);
                    }
                    else
                    {
                        
                        Delivery appointment = (Delivery)element;
                        string sqlStatement = "INSERT INTO Appointments (Name, Type, Date, Duration, CustomerID, Income, Expenses, Balance, Workphone, DestinationAddress, DestinationCity, DestinationZIP, DestinationNote, " +
                                               "Material, Quantity) " +
                                               "VALUES ('" + appointment.name + "', '" + appointment.type + "', '" + appointment.date + "', '" + appointment.duration + "', '" +
                                               appointment.assignedCustomer.id + "', '" + appointment.income + "', '" + appointment.expenses + "', '" + appointment.balance + "', '" + appointment.workPhoneNumber + "', '" +
                                               appointment.destination.livingAdress + "', '" + appointment.destination.city + "', '" + appointment.destination.ZIP + "', '" + appointment.destination.note + "', '" +
                                               appointment.material + "', '" + appointment.quantity + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(appointment); 
                        
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
                        string sqlStatement = "INSERT INTO Customers (Firstname, Lastname, Type, Address, ZIP, City, Phonenumber, Email, CVR, EAN) " +
                                              "Values ('" + customer.firstName + "', '" + customer.lastName + "', '" + customer.type + "', '" + customer.address.livingAdress + "', '" +
                                              customer.address.ZIP + "', '" + customer.address.city + "', '" + customer.contactInfo.phoneNumber + "', '" + customer.contactInfo.email + "', '" +
                                              customer.CVR + "', '" + customer.EAN + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(customer);
                    }
                    else
                    {
                        PrivateCustomer customer = (PrivateCustomer)element;
                        string sqlStatement = "INSERT INTO Customers (Firstname, Lastname, Type, Address, ZIP, City, Phonenumber, Email) " +
                                              "Values ('" + customer.firstName + "', '" + customer.lastName + "', '" + customer.type + "', '" + customer.address.livingAdress + "', '" +
                                              customer.address.ZIP + "', '" + customer.address.city + "', '" + customer.contactInfo.phoneNumber + "', '" + customer.contactInfo.email + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(customer);
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
                        string sqlStatement = "INSERT INTO Equipments (Name, Type, Model, Plates) " +
                                              "Values ('" + equipment.name + "', '" + equipment.type + "', '" + 
                                              equipment.model + "', '" + equipment.regNum + "')";
                        cnn.Execute(sqlStatement);
                        RetrieveSpecificElementIDFromDatabase(equipment);
                    }
                    else
                    {
                        Gear equipment = (Gear)element;
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
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    dynamic output = cnn.QuerySingle<dynamic>("SELECT * FROM Appointments WHERE ID = (SELECT MAX(ID) FROM Appointments)");
                    ((Appointment)element).id = (int)output.ID;
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
        public static dynamic LoadAllOfSpecificElementFromDatabase(string element)
        {
            if (element == "Employee")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Employee>("select * from Employees", new DynamicParameters()).ToList();
                    
                    dynamic realOutput = output;
                    return realOutput;
                }
            }
            else if (element == "Appointment")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Appointment>("select * from Appointments", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else if (element is "Customer")
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Customer>("select * from Customers", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else if (element is "Equipment")
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
                               "set Wage = '" + employee.wage +
                               "', Firstname = '" + employee.firstName +
                               "', Lastname = '" + employee.lastName +
                               "', Email = '" + employee.contactInfo.email +
                               "', Phonenumber = '" + employee.contactInfo.phoneNumber +
                               "', Address = '" + employee.address.livingAdress +
                               "', ZIP = '" + employee.address.ZIP +
                               "', City = '" + employee.address.city +
                               "', AppointmentIDs = '" + employee.appointmentIdString + "' where " +
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
                               "set AppointmentIDs = '" + equipment.appointmentIdString +
                               "', Name = '" + equipment.name +
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
        private static void UpdateAppointment(Appointment appointment)
        {
            string sqlStatement = null;
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                sqlStatement = "update Appointments " +
                               "set Name = '" + appointment.name +
                               "', Type = '" + appointment.type +
                               "', Duration = '" + appointment.duration +
                               "', EmployeeIDs = '" + appointment.employeesIdString +
                               "', EquipmentIDs = '" + appointment.equipmentsIdString +
                               "', CustomerID = '" + appointment.assignedCustomer.id +
                               "', DestinationAddress = '" + appointment.destination.livingAdress +
                               "', DestinationCity = '" + appointment.destination.city +
                               "', DestinationZIP = '" + appointment.destination.ZIP +
                               "', DestinationNote = '" + appointment.destination.note + 
                               "', Income = '" + appointment.income +
                               "', Expenses = '" + appointment.expenses +
                               "', Balance = '" + appointment.balance +
                               "', Date = '" + appointment.date.ToString() +
                               "', Workphone = '" + appointment.workPhoneNumber + "' where " +
                               "ID = " + appointment.id;
                cnn.Execute(sqlStatement);
                if (appointment is Moving)
                {
                    sqlStatement = "update Appointments " +
                                   "set StartingAddress = '" + ((Moving)appointment).startingAddress.livingAdress +
                                   "', StartingCity = '" + ((Moving)appointment).startingAddress.city +
                                   "', StartingZIP = '" + ((Moving)appointment).startingAddress.ZIP +
                                   "', StartingNote = '" + ((Moving)appointment).startingAddress.note + 
                                   "', LentBoxes = '" + ((Moving)appointment).lentBoxes + "' where " +
                               "ID = " + appointment.id;
                    cnn.Execute(sqlStatement);
                }
                if (appointment is Delivery)
                {
                    sqlStatement = "update Appointments " +
                                   "set Material = '" + ((Delivery)appointment).material +
                                   "', Quantity = '" + ((Delivery)appointment).quantity + "' where " +
                               "ID = " + appointment.id;
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
                               "set Firstname = '" + customer.firstName +
                               "', Lastname = '" + customer.lastName +
                               "', Type = '" + customer.type +
                               "', Address = '" + customer.address.livingAdress +
                               "', ZIP = '" + customer.address.ZIP +
                               "', City = '" + customer.address.city +
                               "', Email = '" + customer.contactInfo.email +
                               "', Phonenumber = '" + customer.contactInfo.phoneNumber + "' where " +
                               "ID = " + customer.id;
                cnn.Execute(sqlStatement);
                if (customer is Business)
                {
                    sqlStatement = "update Customers " +
                                   "set EAN = '" + ((Business)customer).EAN +
                                   "', CVR = '" + ((Business)customer).CVR + "' where " +
                               "ID = " + customer.id;
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
