using MudCeramWorkshop.Data.Domain.Models.MainDomain;

namespace MudCeramWorkshop.Data.Domain.InterfacesRepository
{
    public interface IProductMaterialRepository : IGenericRepository<ProductMaterial, int>
    {
        Task<ICollection<ProductMaterial>> GetAll(int idProduct, CancellationToken cancellationToken = default);
    }
}
