using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.Models.Identity;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using MudCeramWorkshop.Data.Repository.Maps.MainDomain;
using MudCeramWorkshop.Data.Repository.Maps.WorkshopDomain;

namespace MudCeramWorkshop.Data.Repository;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Material> Materials { get; set; } = default!;
    public DbSet<Firing> Firings { get; set; } = default!;

    public DbSet<ImageInstruction> ImageInstruction { get; set; } = default!;

    public DbSet<ProductMaterial> ProductMaterials { get; set; } = default!;
    public DbSet<ProductFiring> ProductFirings { get; set; } = default!;

    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;
    public DbSet<Workshop> Workshops { get; set; } = default!;
    public DbSet<WorkshopParameter> WorkshopParameters { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        WorkshopMap.Build(builder);
        IdentityMap.Build(builder);

        ProductMap.Build(builder);
        ImageInstructionMap.Build(builder);
        MaterialMap.Build(builder);
        FiringMap.Build(builder);
        ProductMaterialMap.Build(builder);
        ProductFiringMap.Build(builder);

        base.OnModelCreating(builder);
    }
}