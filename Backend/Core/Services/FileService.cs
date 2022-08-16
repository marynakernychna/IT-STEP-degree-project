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
            var fileName = GenerateFileName();
            var filePath = GenerateNewFilePath(fileName, extension);
            var imageBytes = Convert.FromBase64String(imageBase64String);

            File.WriteAllBytes(filePath, imageBytes);

            return fileName + extension;
        }

        public string GenereteBase64(string imagePath)
        {
            var projectDirectoryPath = GetProjectDirectoryPath();
            var filePath = projectDirectoryPath + WARES_IMAGES_PATH + imagePath;
            var imageBytes = File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(imageBytes);
            var fileExtension = Path.GetExtension(filePath).TrimStart('.');

            return "data:image/" + fileExtension + ";base64," + base64String;
        }

        private static string GenerateFileName()
        {
            return Guid.NewGuid().ToString();
        }

        private static string GetProjectDirectoryPath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            return projectDirectory;
        }

        private static string GenerateNewFilePath(string fileName, string extension)
        {
            var projectDirectoryPath = GetProjectDirectoryPath();
            var folderPath = projectDirectoryPath + WARES_IMAGES_PATH;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath + fileName + extension;
        }
    }
}
