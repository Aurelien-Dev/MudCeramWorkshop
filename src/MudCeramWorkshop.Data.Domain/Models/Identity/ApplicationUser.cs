using Microsoft.AspNetCore.Identity;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using System.ComponentModel.DataAnnotations.Schema;

namespace MudCeramWorkshop.Data.Domain.Models.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("UserAssociation")]
        public int WorkshopId { get; set; }

        public virtual Workshop UserWorkshop { get; set; }
    }
}
