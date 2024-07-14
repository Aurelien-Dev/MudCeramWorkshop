﻿using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.Identity;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using MudCeramWorkshop.Data.Repository.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class WorkshopRepository : GenericRepository<Workshop, int>, IWorkshopRepository
{
    public WorkshopRepository(ApplicationDbContext context) : base(context) { }


    public async Task<Workshop> GetWorkshopInformationForLogin(string email, CancellationToken cancellationToken = default)
    {
        ApplicationUser appUser = await Context.ApplicationUsers.Include(w => w.UserWorkshop).FirstAsyncWait(w => w.Email == email, cancellationToken);
        return appUser.UserWorkshop;
    }

    public async Task<Workshop> GetWorkshopInformationForLogin(int id, CancellationToken cancellationToken = default)
    {
        Workshop workshop = await Context.Workshops.FirstAsyncWait(w => w.Id == id, cancellationToken);
        return workshop;
    }
}