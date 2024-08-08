using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.Identity;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using MudCeramWorkshop.Data.Repository.Utils.Exceptions;
using MudCeramWorkshop.Data.Repository.Utils.Extensions;

namespace MudCeramWorkshop.Data.Repository.Repositories;

public class WorkshopRepository(ApplicationDbContext context) : GenericRepository<Workshop, int>(context), IWorkshopRepository
{
    public async Task<Workshop> GetWorkshopInformationForLogin(string email, CancellationToken cancellationToken = default)
    {
        ApplicationUser appUser = await context.ApplicationUsers.Include(w => w.UserWorkshop).FirstAsyncWait(w => w.Email == email, cancellationToken);

        if (appUser.UserWorkshop == null)
        {
            throw new WorkshopInvalidDataException("Warning : A user must have a workshop.");
        }

        return appUser.UserWorkshop;
    }

    public async Task<Workshop> GetWorkshopInformationForLogin(int id, CancellationToken cancellationToken = default)
    {
        Workshop workshop = await context.Workshops.FirstAsyncWait(w => w.Id == id, cancellationToken);
        return workshop;
    }
}