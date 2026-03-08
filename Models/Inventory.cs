using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [ForeignKey("Equipment")]
        [DisplayName("Equipment")]
        [Column("Equipment ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select the Equipment Name."))]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        [ForeignKey("Vendor")]
        [DisplayName("Vendor")]
        [Column("Vendor ID")]
        //[Range(1, int.MaxValue, ErrorMessage = ("Please select the Vendor Name."))]
        public int VenId { get; set; }
        public Vendor Vendor { get; set; }

        [ForeignKey("Department")]
        [DisplayName("Department")]
        [Column("Department ID")]
        //[Range(1, int.MaxValue, ErrorMessage = ("Please select a department."))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


    }
}
