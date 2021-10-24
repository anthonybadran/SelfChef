using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Models
{
    public class Cuisines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID (Internal)")]
        public int CuisineID { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
