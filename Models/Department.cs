using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "Please enter the Department Name")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(15, ErrorMessage = "Name you mustn't exceed 15 characters.")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Please enter the Department Description ")]
        [MinLength(5, ErrorMessage = "Name mustn't be less than 5 characters.")]

        public string DedpartmentDescription { get; set; }

        // relation between Department table & user & Engineer tables

        public ICollection<Engineer> Engineers { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<Maintenance> Maintenances { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
