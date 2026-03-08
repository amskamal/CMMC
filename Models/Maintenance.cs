using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class Maintenance
    {
        [Key]
        public int MaintenaceId { get; set; }
        
        [Required(ErrorMessage ="Enter the Maintenance Done on date.")]
        [DataType(DataType.Date)]
        [DisplayName("Preventive Maintenance Done On:")]
        public DateTime MaintenaceDoneOn { get; set; }

        [Required(ErrorMessage = "Enter the Maintenance Due on date.")]
        [DataType(DataType.Date)]
        [DisplayName("Preventive Maintenance Due On:")]
        public DateTime MaintenaceDueOn { get; set; }

        [Required(ErrorMessage = "Please enter the remarkes of the service.")]
        [DisplayName("Remarks of engineer before rectification")]
        public string EngRemarks { get; set; }

        [Required(ErrorMessage = "Please enter the remarkes of the service.")]
        [DisplayName("Remarks of user before rectification")]
        public string UserRemarks { get; set; }

        [Required(ErrorMessage = "Enter the BreakDown date.")]
        [DataType(DataType.Date)]
        [DisplayName("Break Down Date:")]
        public DateTime BreakDownDate { get; set; }

        [Required(ErrorMessage = "Please enter the BreakDown details. ")]
        [MinLength(5, ErrorMessage = "Name mustn't be less than 5 characters.")]
        public string BreakDownDetails { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Rectification Date:")]
        public DateTime RectDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Using After Rectification Date:")]
        public DateTime UsingAfterRectDate { get; set; }

        [MinLength(5, ErrorMessage = "Name mustn't be less than 5 characters.")]
        public string RectDetails { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The check up Cost must be in range between 0 and 70K.")]
        [DisplayName("Rectification Cost")]
        public double RectCost { get; set; }

        [DisplayName("Remarks of engineer after rectification")]
        public string RecEngRemarks { get; set; }

        [DisplayName("Remarks of user after rectification")]
        public string RecUserRemarks { get; set; }

        [DisplayName("Engineer's performaance")]
        public string EngPerformance { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName("Down Time:")]
        public int DownTime { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName("Response Time:")]
        public int ResponseTime { get; set; }

        [ForeignKey("Department")]
        [DisplayName("Department")]
        [Column("Department ID")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        [ForeignKey("Engineer")]
        [DisplayName("Engineer")]
        [Column("Engineer ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select the Engineer Name."))]
        public int EngineerId { get; set; }
        public Engineer Engineer { get; set; }


        [ForeignKey("User")]
        [DisplayName("User")]
        [Column("User ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select the User Name."))]
        public int UId { get; set; }
        public User User { get; set; }

        [ForeignKey("Vendor")]
        [DisplayName("Vendor")]
        [Column("Vendor ID")]
        public int VenId { get; set; }
        public Vendor Vendor { get; set; }

        [ForeignKey("Equipment")]
        [DisplayName("Equipment")]
        [Column("Equipment ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select the Equipment Name."))]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

    }
}
