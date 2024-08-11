using MudCeramWorkshop.Data.Domain.Models.MainDomain;

namespace MudCeramWorkshop.Data.Domain.InterfacesRepository;

public interface IProductFiringRepository : IGenericRepository<ProductFiring, int>
{
    Task<ICollection<ProductFiring>> GetAll(int idProduct, CancellationToken cancellationToken = default);
}
