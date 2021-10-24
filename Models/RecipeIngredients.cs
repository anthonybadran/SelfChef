using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Models
{
    [Table("Recipe_Ingredients")]
    public class RecipeIngredients
    {
        [Key]
        [Display(Name = "Recipe")]
        public int RecipeID { get; set; }

        [Key]
        [Display(Name = "Ingredient No.")]
        public int IngredientNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Serving")]
        public string Serving { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
