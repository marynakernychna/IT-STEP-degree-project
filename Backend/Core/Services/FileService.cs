﻿using Core.Interfaces.CustomService;
using System;
using System.Drawing;
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
            var fileName = GenerateFileName(extension);
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
            var imageBytes = File.ReadAllBytes(
                projectDirectoryPath + WARES_IMAGES_PATH + imagePath);
            var base64String = Convert.ToBase64String(imageBytes);

            return "data:image/jpg;base64," + base64String;
        }
    }
}
