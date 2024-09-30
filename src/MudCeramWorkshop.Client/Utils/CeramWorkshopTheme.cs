using MudBlazor;

namespace MudCeramWorkshop.Client.Utils
{
    public static class CeramWorkshopTheme
    {
        public static readonly MudTheme CeramTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight
            {
                Primary = Colors.LightBlue.Darken2,
                AppbarBackground = "#0F3F59",
            },
            Typography = new Typography()
            {
                H6 = new H6()
                {
                    FontSize = "1.2rem"
                }
            }
        };
    }
}