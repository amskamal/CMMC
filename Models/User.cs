using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter the User Name")]
        [MinLength(3, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(15, ErrorMessage = "Name you mustn't exceed 15 characters.")]
        [DisplayName("User Full Name")]
        public string UserFullName { get; set; }

        [Required(ErrorMessage = "please enter an Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DisplayName("User's Email")]
        public string UserEmail { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "please enter an Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [Compare("UserEmail", ErrorMessage = "the email and the conformed email does not match.")]
        public string UserConfirmEmaill { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid phone number.")]
        [DisplayName("User's phone number")]
        public string UserPhoneNo { get; set; }

        [ForeignKey("Department")]
        [DisplayName("Department")]
        [Column("Department ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select a department."))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }
    }
}
