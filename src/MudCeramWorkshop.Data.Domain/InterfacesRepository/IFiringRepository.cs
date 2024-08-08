using MudCeramWorkshop.Data.Domain.Models.MainDomain;

namespace MudCeramWorkshop.Data.Domain.InterfacesRepository;

public interface IFiringRepository : IGenericRepository<Firing, int>
{
    Task<ICollection<Firing>> GetAll(string textSearch, CancellationToken cancellationToken = default);
    Task<int> Count(CancellationToken cancellationToken = default);
}