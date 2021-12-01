using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Models
{
    public class Favorites
    {
        [Key]
        [Display(Name = "Author")]
        public string AuthorID { get; set; }

        [Key]
        [Display(Name = "Recipe")]
        public int RecipeID { get; set; }
    }
}
