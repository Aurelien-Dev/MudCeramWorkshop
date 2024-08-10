using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MudCeramWorkshop.Client.Utils
{
    public class UserService(AuthenticationStateProvider AuthenticationStateProvider)
    {
        public async Task<UserInfoState> GetUserState()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            string? wName = user.FindFirst("WorkshopName")?.Value;
            int? wId = null;

            Claim? claimWId = user.FindFirst("WorkshopId");
            if (claimWId != null)
                wId = Convert.ToInt16(claimWId.Value);

            if (user.Identity is null)
                throw new InvalidOperationException();

            return new UserInfoState(wName, wId, user.Identity.IsAuthenticated);
        }
    }

    public class UserInfoState
    {
        private string? WorkshopName { get; set; }
        private int? WorkshopId { get; set; }
        public bool IsAuthenticated { get; internal set; }

        public UserInfoState(string? workshopName, int? workshopId, bool isAuthenticated)
        {
            WorkshopName = workshopName;
            WorkshopId = workshopId;
            IsAuthenticated = isAuthenticated;
        }

        public string GetWorkshopName()
        {
            return WorkshopName ?? string.Empty;
        }

        public int GetWorkshopId()
        {
            if (WorkshopId == null) throw new InvalidOperationException();

            return (int)WorkshopId;
        }
    }
}