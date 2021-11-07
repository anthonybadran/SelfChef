using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Models
{
    public class Recipes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID (Internal)")]
        public int RecipeID { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Prep. Time")]
        public double PrepTime { get; set; }

        [Required]
        [Display(Name = "Cook Time")]
        public double CookTime { get; set; }

        [Required]
        [Display(Name = "Servings")]
        public double Servings { get; set; }
        
        public string Picture { get; set; }

        [Display(Name = "Private")]
        public bool Private { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Required]
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; }

        [Required]
        [Display(Name = "Directions")]
        public string Directions { get; set; }
        
        public string PictureName { get; set; }

        public string Category { get; set; }

        public string Cuisine { get; set; }
    }
}
