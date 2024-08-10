using Microsoft.AspNetCore.Components;

namespace MudCeramWorkshop.Client.Utils.ComponentBase
{
    public class CustomLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
        }
    }
}
