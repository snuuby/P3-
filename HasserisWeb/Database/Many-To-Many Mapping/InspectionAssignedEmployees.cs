namespace HasserisWeb
{
    public class InspectionAssignedEmployees
    {
        public int InspectionID { get; set; }
        public int EmployeeID { get; set; }
        public InspectionReport InspectionReport { get; set; }
        public Employee Employee { get; set; }
    }
}
