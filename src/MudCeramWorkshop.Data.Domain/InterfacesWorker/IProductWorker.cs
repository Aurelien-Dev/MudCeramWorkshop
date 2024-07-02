using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.InterfacesWorker;

namespace Domain.InterfacesWorker;

public interface IProductWorker : IWorkerBase
{
    IProductRepository ProductRepository { get; }
    IMaterialRepository MaterialRepository { get; }
    IFiringRepository FiringRepository { get; }
    IImageInstructionRepository ImageInstructionRepository { get; }
}
