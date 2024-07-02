using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Repository.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class MaterialRepository : GenericRepository<Material, int>, IMaterialRepository
{
    public MaterialRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Material>> GetAll(EnumMaterialType type, CancellationToken cancellationToken = default)
    {
        return await Context.Materials
            .Include(m => m.ProductMaterial)
            .Where(p => p.Type == type)
            .ToListAsyncWait(cancellationToken);
    }

    public async Task<(IEnumerable<Material>, int)> GetAllWithPaging(EnumMaterialType type, int pageNumber, int pageSize, string sortByName, string sortDirection, CancellationToken cancellationToken = default)
    {
        return await GetAllWithPaging(type, string.Empty, pageNumber, pageSize, sortByName, sortDirection, cancellationToken);
    }

    public async Task<(IEnumerable<Material>, int)> GetAllWithPaging(EnumMaterialType type, string textSearch, int pageNumber, int pageSize, string sortByName, string sortDirection, CancellationToken cancellationToken = default)
    {
        IQueryable<Material> query = Context.Materials
            .Where(p => p.Type == type)
            .AsQueryable();

        //if (!string.IsNullOrWhiteSpace(textSearch))
        //    query = query.Where(p => EF.Functions.ILike(p.Name + p.Reference, $"%{textSearch}%"));

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
        var matToUpdate = await Context.Materials
            .Include(p => p.ProductMaterial)
            .SingleAsyncWait(m => m.Id == idMat, cancellationToken);

        foreach (var item in matToUpdate.ProductMaterial)
        {
            item.Cost = matToUpdate.Cost;
        }
    }
}