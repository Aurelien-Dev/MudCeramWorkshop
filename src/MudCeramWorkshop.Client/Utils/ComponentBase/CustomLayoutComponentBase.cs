using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

namespace MudCeramWorkshop.Client.Utils.ComponentBase
{
    public class CustomLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] IWorkshopRepository WorkshopRepository { get; set; } = default!;
        [CascadingParameter] Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;

        public Workshop Workshop { get; set; } = default!;
        public bool IsAuthenticate { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            IsAuthenticate = user?.Identity?.IsAuthenticated ?? false;

            if (IsAuthenticate && Workshop == null)
            {
                Workshop = await WorkshopRepository.GetWorkshopInformationForLogin(user!.Identity!.Name!);
            }
        }
    }
}
