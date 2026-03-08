using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class Equipment
    {
        [Key]
        public int EqId { get; set; }

        [Required(ErrorMessage ="please enter the Equipment serial no.")]
        //[MinLength(8, ErrorMessage = "Serial No must equals 8 characters.")]
        //[MaxLength(8,ErrorMessage = "Serial No must equals 8 characters.")]
        [DisplayName("Equipment Serial No")]
        public int EqSerialNo { get; set; }

        [Required(ErrorMessage ="please enter the Equipment Name.")]
        [MinLength(3,ErrorMessage = "Name must equals 8 characters.")]
        [DisplayName("Equipment Name")]
        public string EqName { get; set; }

        [Required(ErrorMessage = "Please enter the Installation date.")]
        [DisplayName("Installation Date")]
        [Column("Visiting Time")]
        public DateTime EqInstallatonDate { get; set; }

        [Required(ErrorMessage = "Please enter the Quantity.")]
        [DisplayName("Quantity")]
        [Range(1,int.MaxValue,ErrorMessage ="The Quantity number mustn't less than one.")]
        public int EqQuantity { get; set; }

        [Required(ErrorMessage = "Please enter the cost.")]
        [DisplayName("Cost")]
        [Range(1, int.MaxValue, ErrorMessage = "The Cost mustn't less than one.")]
        public int EqCost { get; set; }

        [Required(ErrorMessage = "Please enter the Warranty Date.")]
        [DisplayName("Warranty Date")]
        public DateTime EqWarrantyDate { get; set; }

        [Required(ErrorMessage = "Please enter the Current Status.")]
        [DisplayName("Current Status")]
        public string EqCurrentStatus { get; set; }

        [ForeignKey("Vendor")]
        [DisplayName("Vendor")]
        [Column("Vendor ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select a Vendor."))]
        public int VenId { get; set; }
        public Vendor Vendor { get; set; }

        [ForeignKey("Department")]
        [DisplayName("Department")]
        [Column("Department ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select a department."))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<Contract> Contracts { get; set; }




    }
}
