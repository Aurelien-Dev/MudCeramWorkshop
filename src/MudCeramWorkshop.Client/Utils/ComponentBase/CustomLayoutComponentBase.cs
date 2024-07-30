using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudCeramWorkshop.Data.Domain.InterfacesWorker;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

namespace MudCeramWorkshop.Client.Utils.ComponentBase
{
    public class CustomLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Inject] IWorkshopWorker WorkshopWorker { get; set; } = default!;

        [CascadingParameter] Task<AuthenticationState> authenticationStateTask { get; set; }


        public Workshop? Workshop { get => _workshop; set => _workshop = value; }
        private Workshop? _workshop = null;
        public bool IsAuthenticate { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated && _workshop is null)
                _workshop = await WorkshopWorker.WorkshopRepository.GetWorkshopInformationForLogin(user.Identity.Name);

            IsAuthenticate = user.Identity.IsAuthenticated;
        }
    }
}
