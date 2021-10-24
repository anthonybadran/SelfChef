using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
    }
}
