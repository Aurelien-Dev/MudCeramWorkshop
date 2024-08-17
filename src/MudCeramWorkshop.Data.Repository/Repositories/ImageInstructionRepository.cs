using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class ImageInstructionRepository : GenericRepository<ImageInstruction, int>, IImageInstructionRepository
{
    private readonly ApplicationDbContext context;

    public ImageInstructionRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IList<ImageInstruction>> GetAll(int productId, CancellationToken cancellationToken = default) => await context.ImageInstruction
        .Where(i => i.IdProduct == productId)
        .ToListAsyncWait(cancellationToken);

    public async Task<ICollection<ImageInstruction>> GetAllNonExported(CancellationToken cancellationToken = default) => await context.ImageInstruction
        .Where(i => i.FileLocation == EnumLocation.Server)
        .ToListAsyncWait(cancellationToken);

    public async Task<ImageInstruction?> GetFavoritImageByProduct(int idProduct, CancellationToken cancellationToken = default)
    {
        bool hasFavorite = await context.ImageInstruction
            .AnyAsyncWait(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

        if (hasFavorite)
            return await context.ImageInstruction
                .FirstAsyncWait(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

        return await context.ImageInstruction
            .FirstOrDefaultAsyncWait(i => i.IdProduct == idProduct, cancellationToken);
    }

    public async Task SetNewFavorite(bool isFavorite, int id, int idProduct, CancellationToken cancellationToken = default)
    {
        ImageInstruction? newFavorite = await context.ImageInstruction
            .FirstOrDefaultAsyncWait(i => i.IdProduct == idProduct && i.Id == id, cancellationToken);

        if (!isFavorite && newFavorite != null)
        {
            newFavorite.IsFavoriteImage = false;
            context.Update(newFavorite);

            await context
                .SaveChangesAsync(cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);
            return;
        }

        ImageInstruction? image = await context.ImageInstruction
            .FirstOrDefaultAsyncWait(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

        if (image != null)
        {
            image.IsFavoriteImage = false;
            context.Update(image);
        }

        if (newFavorite != null)
        {
            newFavorite.IsFavoriteImage = true;
            context.Update(newFavorite);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}