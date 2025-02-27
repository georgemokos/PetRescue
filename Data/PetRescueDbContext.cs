using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetRescue.Models;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity;

namespace PetRescue.Data
{
    public class PetRescueDbContext : IdentityDbContext<IdentityUser>
    {
        public PetRescueDbContext(DbContextOptions<PetRescueDbContext> options)
                 : base(options) { }

        public DbSet<FavoritePet> FavoritePets { get; set; }



       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);
         }
    }


        
 }

