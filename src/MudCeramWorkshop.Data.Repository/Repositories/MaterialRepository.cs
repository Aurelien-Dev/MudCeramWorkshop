using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class MaterialRepository(ApplicationDbContext context) : GenericRepository<Material, int>(context), IMaterialRepository
{
    public async Task<int> Count(EnumMaterialType type, CancellationToken cancellationToken = default)
    {
        return await context.Materials.CountAsync(p => p.Type == type, cancellationToken);
    }

    public async Task<ICollection<Material>> GetAll(EnumMaterialType type, CancellationToken cancellationToken = default)
    {
        return await context.Materials
            .Include(m => m.ProductMaterial)
            .Where(p => p.Type == type)
            .ToListAsyncWait(cancellationToken);
    }

    public async Task<ICollection<Material>> GetAll(EnumMaterialType type, string textSearch, CancellationToken cancellationToken = default)
    {
        IQueryable<Material> query = context.Materials
                .Where(p => p.Type == type)
                .AsQueryable();


        if (!textSearch.IsNullOrEmpty())
        {
            string formatedText = textSearch.Trim();
            query = query.Where(p => p.Name.Contains(formatedText) ||
                                     p.Reference.Contains(formatedText) ||
                                     p.Comment.Contains(formatedText));
        }
        return await query.ToListAsyncWait(cancellationToken);
    }

    public async Task<(IEnumerable<Material>, int)> GetAllWithPaging(EnumMaterialType type, int pageNumber, int pageSize, string sortByName, string sortDirection, CancellationToken cancellationToken = default)
    {
        return await GetAllWithPaging(type, string.Empty, pageNumber, pageSize, sortByName, sortDirection, cancellationToken);
    }

    public async Task<(IEnumerable<Material>, int)> GetAllWithPaging(EnumMaterialType type, string textSearch, int pageNumber, int pageSize, string sortByName, string sortDirection, CancellationToken cancellationToken = default)
    {
        IQueryable<Material> query = context.Materials
            .Where(p => p.Type == type)
            .AsQueryable();

        if (sortDirection != "None")
            query = AddSorting(query, sortDirection, sortByName);

        int total = await query.CountAsyncWait(cancellationToken);

        var result = await query
            .Include(m => m.ProductMaterial)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsyncWait(cancellationToken);

        return (result, total);
    }


    public async Task UpdateAllMaterialCost(int idMat, CancellationToken cancellationToken = default)
    {
        var matToUpdate = await context.Materials
            .Include(p => p.ProductMaterial)
            .SingleAsyncWait(m => m.Id == idMat, cancellationToken);

        foreach (var item in matToUpdate.ProductMaterial)
        {
            item.Cost = matToUpdate.Cost;
        }
    }
}