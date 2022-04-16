using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Z_Store.Models
{
    public class DefaultUser:IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public string Address { get; set; }
        
        [PersonalData]
        public string City { get; set; }

        [PersonalData]
        [DataType(DataType.DateTime)]

        public DateTime UserCreatedDate { get; set; } = DateTime.Now;

    }
}
