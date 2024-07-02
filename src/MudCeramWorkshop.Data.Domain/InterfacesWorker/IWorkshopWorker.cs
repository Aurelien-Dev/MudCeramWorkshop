using MudCeramWorkshop.Data.Domain.InterfacesRepository;

namespace MudCeramWorkshop.Data.Domain.InterfacesWorker;

public  interface IWorkshopWorker : IWorkerBase
{
    IWorkshopRepository WorkshopRepository { get; }
}