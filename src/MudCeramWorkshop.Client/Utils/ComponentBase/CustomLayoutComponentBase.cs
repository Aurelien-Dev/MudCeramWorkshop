using Microsoft.AspNetCore.Components;

namespace MudCeramWorkshop.Client.Utils.ComponentBase
{
    public class CustomLayoutComponentBase : LayoutComponentBase
    {

        [Inject] public SessionInfo Session { get; set; }

    }
}
