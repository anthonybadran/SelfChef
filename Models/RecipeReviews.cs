using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Models
{
    [Table("Recipe_Reviews")]
    public class RecipeReviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID (Internal)")]
        public int ReviewID { get; set; }
        
        [Required]
        [Display(Name = "Recipe")]
        public int RecipeID { get; set; }

        [Required]
        [Display(Name = "Author")]
        public int AuthorID { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string Picture { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
