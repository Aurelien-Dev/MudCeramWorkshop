using OpenAI_API;
using OpenAI_API.Chat;
using System.Reflection;

namespace MudCeramWorkshop.Client.Managers
{
    public class ChatGptManager(IConfiguration Config)
    {
        public async Task<(string, decimal)> GenerateDescription(string imagePath, string materials, string tags, double diameter, double height)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) { throw new ArgumentNullException(nameof(imagePath)); }

            var api = new OpenAIAPI(Config["OpenAiSecret"]);

            string imageBase64 = await DownloadImageAsBase64(imagePath);
            string imageDataUrl = $"data:image/jpeg;base64,{imageBase64}";

            ChatMessage personatPrompt = await GenerateChatMessage("PersonatPrompt", ChatMessageRole.Assistant);
            ChatMessage instructionPrompt = await GenerateChatMessage("DescriptionPrompt", ChatMessageRole.User, imageDataUrl, (s) =>
            {
                return string.Format(s, tags, diameter, height, materials);
            });

            var result = await api.Chat.CreateChatCompletionAsync(
                [
                    personatPrompt,
                    instructionPrompt
                ], model: "gpt-4o-2024-08-06");

            return (result.ToString(), CalculateCost(result));
        }

        public static async Task<ChatMessage> GenerateChatMessage(string promptName, ChatMessageRole chatMessageRole, string? imageDataUrl = null, Func<string, string>? textFormaterAction = null)
        {
            string assistantPrompt = await ReadEmbeddedResourceAsync($"MudCeramWorkshop.Client.Managers.Prompts.{promptName}.txt");
            ChatMessage chatMessage = new ChatMessage(chatMessageRole, string.Empty);

            if (textFormaterAction == null)
                chatMessage.TextContent = assistantPrompt;
            else
                chatMessage.TextContent = textFormaterAction(assistantPrompt);

            if (!string.IsNullOrWhiteSpace(imageDataUrl))
                chatMessage.Images.Add(new ChatMessage.ImageInput(imageDataUrl));

            return chatMessage;
        }


        private static decimal CalculateCost(ChatResult result)
        {
            ////gpt-4o-mini
            //// Tarifs en dollars par token (à ajuster selon les tarifs actuels)
            //decimal costPerInputTokenUSD = 0.150m / 1000000m; // $0.150 / 1M input tokens
            //decimal costPerOutputTokenUSD = 0.600m / 1000000m; // $0.600 / 1M output tokens

            ////gpt-4o
            //// Tarifs en dollars par token (à ajuster selon les tarifs actuels)
            //decimal costPerInputTokenUSD = 5m / 1000000m; // $0.150 / 1M input tokens
            //decimal costPerOutputTokenUSD = 15m / 1000000m; // $0.600 / 1M output tokens

            //gpt-4o-2024-08-06
            // Tarifs en dollars par token (à ajuster selon les tarifs actuels)
            decimal costPerInputTokenUSD = 2.5m / 1000000m; // $0.150 / 1M input tokens
            decimal costPerOutputTokenUSD = 10m / 1000000m; // $0.600 / 1M output tokens

            // Taux de conversion USD -> EUR
            decimal conversionRate = 0.85m;

            // Obtenir le nombre de tokens utilisés
            int inputTokens = result.Usage.PromptTokens;
            int outputTokens = result.Usage.CompletionTokens;

            // Calculer le coût total en dollars
            decimal totalCostUSD = (inputTokens * costPerInputTokenUSD) + (outputTokens * costPerOutputTokenUSD);

            // Convertir le coût en euros
            decimal totalCostEUR = totalCostUSD * conversionRate;

            return totalCostEUR;
        }

        private static async Task<string> ReadEmbeddedResourceAsync(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
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

        private static async Task<string> DownloadImageAsBase64(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageByte = await client.GetByteArrayAsync(imageUrl);

                return Convert.ToBase64String(imageByte);
            }
        }
    }
}