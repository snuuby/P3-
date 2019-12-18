namespace HasserisWeb
{
    public class TaskAssignedEquipment
    {
        public int TaskID { get; set; }
        public int EquipmentID { get; set; }
        public virtual Task Task { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
