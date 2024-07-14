using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.Models.Identity;

namespace MudCeramWorkshop.Data.Repository.Maps.WorkshopDomain;

public static class IdentityMap
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
           .HasOne(u => u.UserWorkshop)
           .WithOne(u => u.ApplicationUser)
           .HasForeignKey<ApplicationUser>(x => x.WorkshopId);
    }
}