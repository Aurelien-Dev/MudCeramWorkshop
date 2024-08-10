using Microsoft.AspNetCore.Components.Authorization;

namespace MudCeramWorkshop.Client.Utils
{
    public class UserService(AuthenticationStateProvider AuthenticationStateProvider)
    {
        public async Task<UserInfoState> GetUserState()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return new UserInfoState()
            {
                WorkshopName = user.FindFirst("WorkshopName").Value,
                WorkshopId = Convert.ToInt16(user.FindFirst("WorkshopId").Value)
            };
        }
    }

    public class UserInfoState
    {
        public string WorkshopName { get; set; }
        public int WorkshopId { get; set; }
    }
}