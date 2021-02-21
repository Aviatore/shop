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
        
        public string UserAuthId { get; set; }
        
        [Display(Name = "Full Name")]
        public string UserName { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [Display(Name = "Phone number")]
        [Phone]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
