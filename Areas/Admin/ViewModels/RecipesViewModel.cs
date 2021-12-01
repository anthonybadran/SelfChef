using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Areas.Admin.ViewModels
{
    public class RecipesViewModel
    {
        public IEnumerable<Recipes> Recipes { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
