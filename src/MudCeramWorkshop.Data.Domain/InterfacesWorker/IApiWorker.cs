using MudCeramWorkshop.Data.Domain.InterfacesRepository;

namespace MudCeramWorkshop.Data.Domain.InterfacesWorker;

public  interface IApiWorker : IWorkerBase
{
    IImageInstructionRepository ImageInstructionRepository { get; }
}
