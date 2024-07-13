using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.Models.Identity;

namespace MudCeramWorkshop.Data.Identity
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<ApplicationUser>()
            //    .HasOne(u => u.UserWorkshop)
            //    .WithOne()
            //    .HasForeignKey();
                


            base.OnModelCreating(builder);
        }
    }
}
