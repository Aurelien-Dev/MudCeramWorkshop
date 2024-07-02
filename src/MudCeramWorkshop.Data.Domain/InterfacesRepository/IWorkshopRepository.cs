﻿using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

namespace MudCeramWorkshop.Data.Domain.InterfacesRepository;

public interface IWorkshopRepository : IGenericRepository<Workshop, int>
{
    /// <summary>
    /// Retrieves workshop information and checks if the provided email exists in the database for a workshop login.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A tuple containing the Workshop object (or null) and a boolean indicating whether the email exists in the database.</returns>
    Task<(Workshop?, bool)> GetWorkshopInformationForLogin(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the provided email address exists in the Workshops table.
    /// </summary>
    /// <param name="email">The email address to check for existence.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A boolean value indicating whether the email address exists (true) or not (false).</returns>
    Task<bool> CheckIfEmailExists(string email, CancellationToken cancellationToken = default);
}