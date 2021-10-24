using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Models
{
    [Table("Recipe_Directions")]
    public class RecipeDirections
    {
        [Key]
        [Display(Name = "Recipe")]
        public int RecipeID { get; set; }

        [Key]
        [Display(Name = "Direction No.")]
        public int DirectionNo { get; set; }

        [Required]
        [StringLength(400)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
