using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HasserisWeb
{
    public abstract class Task
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<TaskAssignedEmployees> taskAssignedEmployees { get; set; } = new List<TaskAssignedEmployees>();
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Address Destination { get; set; }
        [Required]
        public double Income { get; set; }
        public double Expenses { get; set; }
        public ICollection<TaskAssignedEquipment> taskAssignedEquipment { get; set; } = new List<TaskAssignedEquipment>();
        public ICollection<DateTimes> Dates { get; set; } = new List<DateTimes>();
        //Properties for calculating the total duration of a task, taskDuration.
        [Column(TypeName = "Date")]
        public DateTime StartTime { get; set; }
        [Column(TypeName = "Date")]
        public DateTime EndTime { get; set; }
        public ICollection<PauseTimes> PauseTimes {get; set;} = new List<PauseTimes>();
        public bool IsPaused { get; set; }
        public TimeSpan TaskDuration { get; set; }
        public string Description { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string PhotoPath { get; set; }
        public Task()
        {

        }
        
        public Task(string name, Customer assignedCustomer, Address destination, 
                            double income, List<DateTime> Ldates, string description, string workPhoneNumber)
        {
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

        public void BeginTasks()
        {
            this.StartTime = DateTime.Now;
        }
        public void ResumeTasks() 
        {
            if (IsPaused)
            {
                IsPaused = false;
                StartTime = DateTime.Now;
            }
        }
        public void PauseTasks() 
        {
            if (!IsPaused)
            {
                IsPaused = true;
                DateTime temp = DateTime.Now;
                //On first pause, add the time for task start and now to the list of pauseTimes.
                if (PauseTimes.Count == 0)
                {
                    PauseTimes.Add(new PauseTimes() { Date = StartTime });

                }
                //add the current time to the pause list on every pause
                PauseTimes.Add(new PauseTimes() { Date = temp });
                //also on first pause, make the current duration equal to the difference between now and task start.
                if (PauseTimes.Count == 2)
                {
                    TaskDuration = temp - StartTime;
                }
                //on subsequent pauses, add the difference between now and last resume.
                if (PauseTimes.Count > 2)
                {
                    TaskDuration += temp - StartTime;
                }
            }
        }

        public void EndTasks()
        {
            this.EndTime = DateTime.Now;
            TimeSpan t = EndTime - StartTime;
            if (PauseTimes.Count >= 2) 
            {
                TaskDuration += t;
            }
            else 
            {
                TaskDuration = t;
            }
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
