namespace Core.Interfaces.CustomService
{
    public interface IFileService
    {
        public string CreateWarePhotoFile(string imageBase64String, string extension);
    }
}
