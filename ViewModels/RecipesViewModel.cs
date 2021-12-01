using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.ViewModels
{
    public class RecipesViewModel
    {
        public IEnumerable<Recipes> Recipes { get; set; }
        public IEnumerable<RecipeReviews> Reviews { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
