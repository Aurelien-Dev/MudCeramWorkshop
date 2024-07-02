using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MudCeramWorkshop.Client.IdentityData.Model;

namespace MudCeramWorkshop.Client.IdentityData
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<WorkshopUser>(options)
    {
    }
}
