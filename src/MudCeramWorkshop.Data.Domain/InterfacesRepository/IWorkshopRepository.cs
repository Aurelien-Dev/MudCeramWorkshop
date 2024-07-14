using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

namespace MudCeramWorkshop.Data.Domain.InterfacesRepository;

public interface IWorkshopRepository : IGenericRepository<Workshop, int>
{
    Task<Workshop> GetWorkshopInformationForLogin(string email, CancellationToken cancellationToken = default);

}