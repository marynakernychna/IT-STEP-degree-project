namespace Core.Interfaces.CustomService
{
    public interface IFileService
    {
        string CreateWarePhotoFile(
            string imageBase64String, string extension);
        string GenereteBase64(
            string imagePath);
    }
}
