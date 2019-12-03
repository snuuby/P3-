using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HasserisWeb
{
    public abstract class Task
    {
        //represents phase 3 in the process of an order
        //Goes from VisitingReport -> Offer -> Task. It is collected in this Task class so information can be saved in one object through different phases.
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TaskAssignedEmployees> Employees { get; set; } = new List<TaskAssignedEmployees>();
        public virtual ICollection<TaskAssignedEquipment> Equipment { get; set; } = new List<TaskAssignedEquipment>();
        public virtual InspectionReport InspectionReport { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Address Destination { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
        public virtual ICollection<DateTimes> Dates { get; set; } = new List<DateTimes>();
        public virtual ICollection<PauseTimes> PauseTimes {get; set;} = new List<PauseTimes>();
        public bool IsPaused { get; set; }
        public TimeSpan TaskDuration { get; set; }
        public string Description { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string PhotoPath { get; set; }
        public int Phase { get; set; }
        public bool wasInspection { get; set; }
        public bool wasOffer { get; set; }

        protected Task()
        {

        }

        public Task(string name, Customer assignedCustomer, Address destination,
                            double income, List<DateTime> Ldates, string description, string workPhoneNumber, int phase)
        {
            this.Phase = phase;
            this.Name = name;
            this.Customer = assignedCustomer;
            this.Destination = destination;
            this.Income = income;
            this.Description = description;
            foreach (DateTime date in Ldates)
            {
                DateTimes temp = new DateTimes();
                temp.Date = date;
                Dates.Add(temp);
            }
            this.WorkPhoneNumber = workPhoneNumber;

            this.IsPaused = false;
            this.PhotoPath = "assets/images/tasks/placeholder.png";
        }

  
        
        

        //Just change it if the calculation is more complex (it probably is)
        //Maybe we have to take into account things like "feriepenge" and money spent on fuel etc..
        /*
        private double CalculateBalance()
        {
            double totalCost = 0;
            double employeeSecondWage;
            foreach (Employee employee in Employees)
            {
                employeeSecondWage = (employee.Wage / 60) / 60;
                totalCost += employeeSecondWage * Convert.ToInt32(Math.Round(TaskDuration.TotalSeconds));
            }
            return totalCost;
        }

        //Adds element to appointment (can be both equipment and employee).
        //Also adds this appointment to either employee or equipment object so all we have to do is call this function
        //Also adds this elements id to the id string so we can transfer it to the database to keep track of all elements in this appointment
        public void AddElementToTask(dynamic element)
        {
            if (element is Employee)
            {
                Employees.Add((Employee)element);
                Employee employee = (Employee)element;
            }
            else if (element is Equipment)
            {
                Equipment.Add((Equipment)element);
                Equipment equipment = (Equipment)element;
            }

        }

        //Remove element from appointment (can be both equipment and employee)
        //Also removes the appointment from the element, and removes the elementID in the employeesIdString and equipmentsIdString
        /*
        public void RemoveElementFromTask(dynamic element)
        {
            if (element is Employee)
            {
                foreach (Employee employee in Employees)
                {
                    if (employee.ID == element.id)
                    {
                        Employees.Remove(element);
                        element.RemoveAppointment(this);
                    }
                }
            }
            else if (element is Equipment)
            {
                foreach (Equipment equipment in Equipment)
                {
                    if (equipment.ID == element.id)
                    {
                        Equipment.Remove(element);
                        element.RemoveAppointment(this);
                    }
                }
            }

        }
        */
    }
}
