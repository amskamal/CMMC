using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "Please enter the Contract Date.")]
        [DisplayName("Contract Date")]
        public DateTime ContractDate { get; set; }

        [Required(ErrorMessage = "Please enter the Status")]
        //[MinLength(4, ErrorMessage = "Status mustn't be less than 4 characters.")]
        //[MaxLength(5, ErrorMessage = "Status you mustn't exceed 5 characters.")]
        [DisplayName("Contract Status")]
        public string ContractStatus { get; set; }

        [Required(ErrorMessage = "Please enter the type of the contract.")]
        [DisplayName("Contract Type")]
        public string ContractType { get; set; }

        [Required(ErrorMessage = "Please enter the contract cost.")]
        [DisplayName("Contract Cost")]
        [Range(1, int.MaxValue, ErrorMessage = "The Cost mustn't less than one.")]
        public double ContractCost { get; set; }

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
    }
}
