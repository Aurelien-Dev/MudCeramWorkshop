﻿using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class ProductRepository : GenericRepository<Product, int>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }
    
    public async Task<Product> Get(int id, int idWorkshop, CancellationToken cancellationToken = default)
    {
        return await Context.Products
            .Where(p => p.Id == id && p.IdWorkshop == idWorkshop)
            .Include(p => p.ImageInstructions.OrderByDescending(i => i.IsFavoriteImage))
            .Include(p => p.ProductMaterial)
            .ThenInclude(x => x.Material)
            .Include(p => p.ProductFiring)
            .ThenInclude(x => x.Firing)
            .FirstAsyncWait(cancellationToken);
    }

    public async Task<ICollection<Product>> GetAll(int idWorkshop, CancellationToken cancellationToken = default)
    {
        return await Context.Products
            .Where(p => p.IdWorkshop == idWorkshop)
            .Include(p => p.ImageInstructions)
            .ToListAsyncWait(cancellationToken);
    }

    public async Task<Product> GetLight(object id, CancellationToken cancellationToken = default)
    {
        return await Context.Products
            .Where(p => p.Id == (int)id)
            .FirstAsyncWait(cancellationToken);
    }

    public async Task<int> CountImage(int id, CancellationToken cancellationToken = default)
    {
        return await Context.ImageInstruction
            .Where(i => i.IdProduct == id)
            .CountAsyncWait(cancellationToken);
    }

    public async Task UpdateProductMaterialCostAndQuantity(ProductMaterial productMaterial, CancellationToken cancellationToken = default)
    {
        ProductMaterial pMaterial = await Context.ProductMaterials
            .FirstAsyncWait(p => p.Id == productMaterial.Id, cancellationToken);

        pMaterial.Cost = productMaterial.Cost;
        pMaterial.Quantity = productMaterial.Quantity;

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductFiring(ProductFiring productFiring, CancellationToken cancellationToken = default)
    {
        ProductFiring pFiring = await Context.ProductFirings
            .FirstAsyncWait(p => p.Id == productFiring.Id, cancellationToken);

        pFiring.CostKwH = productFiring.CostKwH;

        await Context.SaveChangesAsync(cancellationToken);
    }
}
