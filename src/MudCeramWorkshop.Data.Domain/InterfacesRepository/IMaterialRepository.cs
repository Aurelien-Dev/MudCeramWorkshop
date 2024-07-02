﻿using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;

namespace MudCeramWorkshop.Data.Domain.InterfacesRepository;

public interface IMaterialRepository : IGenericRepository<Material, int>
{
    /// <summary>
    /// Retrieves a collection of materials filtered by the specified material type.
    /// </summary>
    /// <param name="type">The MaterialType to filter the materials by.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, with a result containing the collection of Material objects matching the specified type.</returns>
    Task<ICollection<Material>> GetAll(EnumMaterialType type, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the cost of all product materials associated with a specific material ID.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <param name="idMat">The material ID to filter by and update associated product material costs.</param>
    Task UpdateAllMaterialCost(int idMat, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a collection of materials filtered by the specified material type.
    /// </summary>
    /// <param name="type">The MaterialType to filter the materials by.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="sortByName">The name of the property to sort the results by.</param>
    /// <param name="sortDirection">The sort direction (ascending or descending).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, with a result containing the collection of Material objects matching the specified type.</returns>
    Task<(IEnumerable<Material>, int)> GetAllWithPaging(EnumMaterialType type, int pageNumber, int pageSize, string sortByName, string sortDirection, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a collection of materials filtered by the specified material type.
    /// </summary>
    /// <param name="type">The MaterialType to filter the materials by.</param>
    /// <param name="textSearch">The text to filter the materials.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="sortByName">The name of the property to sort the results by.</param>
    /// <param name="sortDirection">The sort direction (ascending or descending).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, with a result containing the collection of Material objects matching the specified type.</returns>
    Task<(IEnumerable<Material>, int)> GetAllWithPaging(EnumMaterialType type, string textSearch, int pageNumber, int pageSize, string sortByName, string sortDirection, CancellationToken cancellationToken = default);
}