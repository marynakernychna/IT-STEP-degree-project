namespace Core.Interfaces.CustomService
{
    public interface IFileService
    {
        public string CreateWarePhotoFile(string imageBase64String, string extension);
        public string GenereteBase64(string imagePath);
    }
}
