using Microsoft.AspNetCore.Identity;
using MudCeramWorkshop.Client.IdentityData.Model;

namespace MudCeramWorkshop.Client.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<WorkshopUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<WorkshopUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
