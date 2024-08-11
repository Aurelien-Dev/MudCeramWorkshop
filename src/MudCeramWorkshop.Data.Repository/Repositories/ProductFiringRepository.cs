using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class ProductFiringRepository : GenericRepository<ProductFiring, int>, IProductFiringRepository
{
    private readonly ApplicationDbContext context;

    public ProductFiringRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public virtual async Task<ICollection<ProductFiring>> GetAll(int idProduct, CancellationToken cancellationToken = default)
    {
        return await context.Set<ProductFiring>()
            .Where(p => p.IdProduct == idProduct)
            .Include(p => p.Firing)
            .ToListAsyncWait(cancellationToken);
    }
}