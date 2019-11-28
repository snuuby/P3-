using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class InspectionAssignedEquipment
    {
        public int InspectionID { get; set; }
        public int EquipmentID { get; set; }
        public InspectionReport InspectionReport { get; set; }
        public Equipment Equipment { get; set; }
    }
}
