namespace HasserisWeb
{
    public class TaskAssignedEmployees
    {
        public int TaskID { get; set; }
        public int EmployeeID { get; set; }
        public virtual Task Task { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
