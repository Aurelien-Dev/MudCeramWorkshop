using Microsoft.IdentityModel.Tokens;
using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class FiringRepository : GenericRepository<Firing, int>, IFiringRepository
{
    private readonly ApplicationDbContext context;

    public FiringRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<ICollection<Firing>> GetAll(string textSearch, CancellationToken cancellationToken = default)
    {
        IQueryable<Firing> query = context.Firings.AsQueryable();

        if (!textSearch.IsNullOrEmpty())
        {
            string formatedText = textSearch.Trim();
            query = query.Where(p => p.Name.Contains(formatedText));
        }
        return await query.ToListAsyncWait(cancellationToken);
    }

    public async Task<int> Count(CancellationToken cancellationToken = default)
    {
        return await context.Firings.CountAsyncWait(cancellationToken);
    }
}