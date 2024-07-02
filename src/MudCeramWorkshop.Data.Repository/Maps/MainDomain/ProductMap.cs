using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace MudCeramWorkshop.Data.Repository.Maps.MainDomain;

public static class ProductMap
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
                    .HasOne(s => s.Workshop)
                    .WithMany(g => g.Products)
                    .HasForeignKey(s => s.IdWorkshop);
    }
}