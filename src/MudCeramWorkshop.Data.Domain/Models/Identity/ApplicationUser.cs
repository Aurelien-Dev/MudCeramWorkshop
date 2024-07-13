using Microsoft.AspNetCore.Identity;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

namespace MudCeramWorkshop.Data.Domain.Models.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int WorkshopId { get; set; }

        public virtual Workshop UserWorkshop { get; set; }
    }

}
