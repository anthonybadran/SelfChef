using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.ViewModels
{
    public class FavoritesViewModel
    {
        public IEnumerable<Favorites> Favorites { get; set; }
        public IEnumerable<Recipes> Recipes { get; set; }
    }
}
