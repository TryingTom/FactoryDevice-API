using System;
using System.Collections.Generic;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class FactoryDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }

        // one-to-many relation with differenet maintenances, as one device can have multiple problems
        public ICollection<FactoryDeviceMaintenance> Maintenances { get; set; }
    }
}