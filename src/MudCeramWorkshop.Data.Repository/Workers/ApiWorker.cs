using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.InterfacesWorker;

namespace MudCeramWorkshop.Data.Repository.Workers;

internal class ApiWorker : WorkerBase, IApiWorker
{
    public IImageInstructionRepository ImageInstructionRepository { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Db Context</param>
    /// <param name="imageInstructionRepository">Image Repository</param>
    public ApiWorker(ApplicationDbContext context, IImageInstructionRepository imageInstructionRepository)
        : base(context)
    {
        ImageInstructionRepository = imageInstructionRepository;
    }
}