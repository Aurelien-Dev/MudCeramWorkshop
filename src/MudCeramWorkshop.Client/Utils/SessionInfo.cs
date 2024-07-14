using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

namespace MudCeramWorkshop.Client.Utils
{
    public class SessionInfo
    {
        public Workshop? Workshop { get; set; }
        public bool IsAuthenticate { get; set; } = false;

        public string WorkshopFolderName { get => Workshop.Name.Replace(" ", string.Empty); }
    }
}
