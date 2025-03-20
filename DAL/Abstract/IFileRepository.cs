namespace DAL.Abstract
{
    public interface IFileRepository
    {
        public string UploadFile(string conatinerName, string fileName, Stream fileContent);
        public Stream GetFile(string conatinerName, string fileName);
        public void DeleteFile(string conatinerName, string fileName);
    }
}
