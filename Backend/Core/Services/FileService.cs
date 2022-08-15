using Core.Interfaces.CustomService;
using System;
using System.IO;

namespace Core.Services
{
    public class FileService : IFileService
    {
        const string WARES_IMAGES_PATH = "\\images\\wares\\";

        public FileService()
        { }

        public string CreateWarePhotoFile(string imageBase64String, string extension)
        {
            var fileName = GenerateFileName(extension) + "." + extension;
            var filePath = GenerateNewFilePath(fileName);
            var imageBytes = Convert.FromBase64String(imageBase64String);

            File.WriteAllBytes(filePath, imageBytes);

            return fileName;
        }

        private string GenerateNewFilePath(string fileName)
        {
            var projectDirectoryPath = GetProjectDirectoryPath();
            var folderPath = projectDirectoryPath + WARES_IMAGES_PATH;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath + fileName;
        }

        private string GenerateFileName(string extension)
        {
            return Guid.NewGuid().ToString() + extension;
        }

        private string GetProjectDirectoryPath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            return projectDirectory;
        }

        public string GenereteBase64(string imagePath)
        {
            var projectDirectoryPath = GetProjectDirectoryPath();
            var filePath = projectDirectoryPath + WARES_IMAGES_PATH + imagePath;
            var imageBytes = File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(imageBytes);
            var fileExtension = Path.GetExtension(filePath);

            return "data:image/" + fileExtension + ";base64," + base64String;
        }
    }
}
