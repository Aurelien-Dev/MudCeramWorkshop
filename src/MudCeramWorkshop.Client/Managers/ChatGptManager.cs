using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using OpenAI_API;
using OpenAI_API.Chat;
using System.Reflection;

namespace MudCeramWorkshop.Client.Managers
{
    public class ChatGptManager(IConfiguration Config)
    {
        public async Task<string> GenerateDescription(string imagePath, Product product, List<ProductMaterial> productMaterials)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) { throw new ArgumentNullException(nameof(imagePath)); }
            if (product == null) { throw new ArgumentNullException(nameof(product)); }

            var api = new OpenAIAPI(Config["OpenAiSecret"]);

            string imageBase64 = await DownloadImageAsBase64(imagePath);
            string imageDataUrl = $"data:image/jpeg;base64,{imageBase64}";


            string[] argiles = productMaterials.Where(m => m.Material.Type == EnumMaterialType.Argile).Select(s => s.Material.Name).ToArray();

            ChatMessage personatPrompt = await GenerateChatMessage("PersonatPrompt", ChatMessageRole.Assistant);

            ChatMessage instructionPrompt = await GenerateChatMessage("DescriptionPrompt", ChatMessageRole.User, imageDataUrl, (s) =>
            {
                return string.Format(s, string.Join(", ", product.Tags), product.TopDiameter, product.Height, string.Join(", ", argiles));
            });

            var result = await api.Chat.CreateChatCompletionAsync(
                [
                    personatPrompt,
                    instructionPrompt
                ], model: "gpt-4o");

            return result.ToString();
        }


        public async Task<ChatMessage> GenerateChatMessage(string promptName, ChatMessageRole chatMessageRole, string imageDataUrl = null, Func<string, string> textFormaterAction = null!)
        {
            string assistantPrompt = await ReadEmbeddedResourceAsync($"MudCeramWorkshop.Client.Managers.Prompts.{promptName}.txt");
            ChatMessage d = new ChatMessage(chatMessageRole, string.Empty);

            if (textFormaterAction == null)
                d.TextContent = assistantPrompt;
            else
                d.TextContent = textFormaterAction(assistantPrompt);

            if (!string.IsNullOrWhiteSpace(imageDataUrl))
                d.Images.Add(new ChatMessage.ImageInput(imageDataUrl));

            return d;
        }

        private async Task<string> ReadEmbeddedResourceAsync(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Resource not found", resourceName);
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        private async Task<string> DownloadImageAsBase64(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageByte = await client.GetByteArrayAsync(imageUrl);

                return Convert.ToBase64String(imageByte);
            }
        }
    }
}
