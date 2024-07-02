using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class FiringRepository : GenericRepository<Firing, int>, IFiringRepository
{
    public FiringRepository(ApplicationDbContext context) : base(context) { }
}