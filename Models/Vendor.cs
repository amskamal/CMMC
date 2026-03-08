using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Please enter the Vendor Name")]
        [MinLength(3, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(15, ErrorMessage = "Name you mustn't exceed 15 characters.")]
        [DisplayName("Vendor Full Name")]
        public string VendorFullName { get; set; }

        [Required(ErrorMessage = "please enter an Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DisplayName("Vendor's Email")]
        public string VendorEmail { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "please enter an Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [Compare("VendorEmail", ErrorMessage = "the email and the conformed email does not match.")]
        public string VendorConfirmEmaill { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid phone number.")]
        [DisplayName("Vendor's phone number")]
        public string VendorPhoneNo { get; set; }

        [Required(ErrorMessage = "Please enter the vendor Address.")]
        [DisplayName("Vendor's Address")]
        [MinLength(5,ErrorMessage ="please enter the address completely.")]
        public string VendorAddress { get; set; }

        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<Maintenance> Maintenances { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<Contract> Contracts { get; set; }

    }
}
