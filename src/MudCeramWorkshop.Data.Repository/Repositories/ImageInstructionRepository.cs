using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using MudCeramWorkshop.Data.Repository.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class ImageInstructionRepository : GenericRepository<ImageInstruction, int>, IImageInstructionRepository
{
    public ImageInstructionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ImageInstruction>> GetAllNonExported(CancellationToken cancellationToken = default) => await Context.ImageInstruction
        .Where(i => i.FileLocation == EnumLocation.Server)
        .ToListAsyncWait(cancellationToken);

    public async Task<ImageInstruction?> GetFavoritImageByProduct(int idProduct, CancellationToken cancellationToken = default)
    {
        bool hasFavorite = await Context.ImageInstruction
            .AnyAsyncWait(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

        if (hasFavorite)
            return await Context.ImageInstruction
                .FirstAsyncWait(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

        return await Context.ImageInstruction
            .FirstOrDefaultAsyncWait(i => i.IdProduct == idProduct, cancellationToken);
    }

    public async Task SetNewFavorite(bool isFavorite, int id, int idProduct, CancellationToken cancellationToken = default)
    {
        ImageInstruction? newFavorite = await Context.ImageInstruction
            .FirstOrDefaultAsyncWait(i => i.IdProduct == idProduct && i.Id == id, cancellationToken);

        if (!isFavorite && newFavorite != null)
        {
            newFavorite.IsFavoriteImage = false;
            Context.Update(newFavorite);
            
            await Context
                .SaveChangesAsync(cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);
            return;
        }

        ImageInstruction? image = await Context.ImageInstruction
            .FirstOrDefaultAsyncWait(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

        if (image != null)
        {
            image.IsFavoriteImage = false;
            Context.Update(image);
        }

        if (newFavorite != null)
        {
            newFavorite.IsFavoriteImage = true;
            Context.Update(newFavorite);
        }

        await Context.SaveChangesAsync(cancellationToken);
    }
}