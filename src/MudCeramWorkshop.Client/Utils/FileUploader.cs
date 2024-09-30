using Markdig.Extensions.Tables;
using Microsoft.AspNetCore.Components.Forms;
using MudCeramWorkshop.Client.Utils.Singletons;

namespace MudCeramWorkshop.Client.Utils
{
    public class FileUploader(EnvironementSingleton environement)
    {
        private readonly long MaxFileSize = 1024 * 1024 * 15;
        private readonly string WebRootPathPath = environement.WebRootPath;
        private readonly string ProductFolderFullPath = Path.Combine(environement.WebRootPath, "ProductImages");
        private readonly string ProductFolderShort = Path.Combine("ProductImages");


        /// <summary>
        /// Upload file into server
        /// </summary>
        /// <param name="e">InputFile event argument</param>
        /// <returns>Return path of uploaded img</returns>
        public async Task<string> LoadFileInput(IBrowserFile e, string workshopFolderName)
        {
            try
            {
                string fileName = CreateTempFileName(workshopFolderName, Path.GetExtension(e.Name));
                string workshopFolder = Path.Combine(ProductFolderFullPath, workshopFolderName, fileName);

                if (!Directory.Exists(Path.Combine(ProductFolderFullPath, workshopFolderName)))
                    Directory.CreateDirectory(Path.Combine(ProductFolderFullPath, workshopFolderName));

                await using FileStream fs = new(workshopFolder, FileMode.Create);
                await e.OpenReadStream(MaxFileSize).CopyToAsync(fs);
                fs.Close();

                return Path.Combine(ProductFolderShort, workshopFolderName, fileName);
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                Console.WriteLine($"Error uploading file: {ex.StackTrace}");

                throw new Exception("Error uploading file");
            }
        }

        /// <summary>
        /// Create URI for temporary file in wwwroot/assets
        /// </summary>
        /// <param name="workshopName">Name of workshop</param>
        /// <param name="extension">Original extension file</param>
        /// <returns>Return completed path for asset folder on server</returns>
        private static string CreateTempFileName(string workshopName, string extension)
        {
            var trustedFileNameForFileStorage = $"{workshopName}-{Path.GetRandomFileName()}";
            trustedFileNameForFileStorage = Path.ChangeExtension(trustedFileNameForFileStorage, extension);
            return trustedFileNameForFileStorage;
        }

        public void RemoveFileInput(string filePath)
        {
            if (File.Exists(Path.Combine(WebRootPathPath, filePath)))
                File.Delete(Path.Combine(WebRootPathPath, filePath));
        }
    }
}