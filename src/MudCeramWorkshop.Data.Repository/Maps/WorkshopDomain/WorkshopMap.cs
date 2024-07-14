using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

namespace MudCeramWorkshop.Data.Repository.Maps.WorkshopDomain;

public static class WorkshopMap
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workshop>().HasKey(p => p.Id);
    }
}