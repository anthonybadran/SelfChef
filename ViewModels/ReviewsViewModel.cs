using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.ViewModels
{
    public class ReviewsViewModel
    {
        public IEnumerable<RecipeReviews> Reviews { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<ReviewVotes> ReviewVotes { get; set; }
    }
}
