using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class FactoryDeviceMaintenance
    {
        // Id for 
        public int Id { get; set; }

        // time when reported
        [Required]
        public DateTime TimeReported { get; set; }

        // description of the problem
        [Required]
        public string Description { get; set; }

        // urgency level
        [EnumDataType(typeof(Urgency))]
        [Required]
        //[RegularExpression("^[0-2]*$", ErrorMessage = "Invalid Urgency Type")]
        public Urgency UrgencyLevel { get; set; }

        // three levels of ugency
        public enum Urgency
        {
            Lievät = 0,
            Tärkeät = 1,
            Kriittiset =2
        }

        // boolean value if the problem is fixed or not
        [Required]
        public bool Fixed { get; set; }

        // foreign key for the FactoryDevice
        [Required]
        [ForeignKey("FactoryDeviceId")]
        public int FactoryDeviceId { get; set; }
    }
}
