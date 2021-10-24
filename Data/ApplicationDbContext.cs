using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<RecipeDirections> RecipeDirections { get; set; }
        public DbSet<RecipeIngredients> RecipeIngredients { get; set; }
        public DbSet<RecipeReviews> RecipeReviews { get; set; }
        public DbSet<Cuisines> Cuisines { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<ReviewVotes> ReviewVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.HasKey(e => new { e.AuthorID, e.RecipeID })
                    .HasName("Favorites_PK");
            });

            modelBuilder.Entity<RecipeDirections>(entity =>
            {
                entity.HasKey(e => new { e.RecipeID, e.DirectionNo })
                    .HasName("Recipe_Direction_PK");
            });

            modelBuilder.Entity<RecipeIngredients>(entity =>
            {
                entity.HasKey(e => new { e.RecipeID, e.IngredientNo })
                    .HasName("Recipe_Ingredients_PK");
            });

            modelBuilder.Entity<ReviewVotes>(entity =>
            {
                entity.HasKey(e => new { e.ReviewID, e.AuthorID })
                    .HasName("Review_Votes_PK");
            });
        }
    }
}
