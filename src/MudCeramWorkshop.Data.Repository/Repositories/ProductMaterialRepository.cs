using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class ProductMaterialRepository : GenericRepository<ProductMaterial, int>, IProductMaterialRepository
{
    private readonly ApplicationDbContext context;

    public ProductMaterialRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public virtual async Task<ICollection<ProductMaterial>> GetAll(int idProduct, CancellationToken cancellationToken = default)
    {
        return await context.Set<ProductMaterial>()
            .Where(p => p.IdProduct == idProduct)
            .Include(p => p.Material).OrderByDescending(p => p.Material.Type).ThenBy(p => p.Material.Name)
            .ToListAsyncWait(cancellationToken);
    }
}