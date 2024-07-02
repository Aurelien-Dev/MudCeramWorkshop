﻿using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using MudCeramWorkshop.Data.Repository.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class WorkshopRepository : GenericRepository<Workshop, int>, IWorkshopRepository
{
    public WorkshopRepository(ApplicationDbContext context) : base(context) { }

    public async  Task<(Workshop?, bool)> GetWorkshopInformationForLogin(string email, CancellationToken cancellationToken = default)
    {
        Workshop workshop = await Context.Workshops.FirstOrDefaultAsyncWait(w => w.Email == email, cancellationToken);
        
        bool mailExist = workshop != null && email == workshop.Email;

        return (workshop, mailExist);
    }

    public async Task<bool> CheckIfEmailExists(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Workshops.AnyAsyncWait(w => w.Email == email, cancellationToken);
    }
}