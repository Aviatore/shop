using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace shop.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        
        [Display(Name = "Full Name")]
        [Required]
        public string UserName { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [Display(Name = "Phone number")]
        [Phone]
        [Required]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
