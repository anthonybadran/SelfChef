using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Models
{
    [Table("Review_Votes")]
    public class ReviewVotes
    {
        [Key]
        [Display(Name = "Review")]
        public int ReviewID { get; set; }

        [Key]
        [Display(Name = "Author")]
        public string AuthorID { get; set; }

        [Required]
        [Display(Name = "Vote")]
        public int Vote { get; set; }
    }
}
