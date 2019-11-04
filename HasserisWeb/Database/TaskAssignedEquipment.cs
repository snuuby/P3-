using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class TaskAssignedEquipment
    {
        public int TaskID { get; set; }
        public int EquipmentID { get; set; }
        public Task Task { get; set; }
        public Equipment Equipment { get; set; }
    }
}
