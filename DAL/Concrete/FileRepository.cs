using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DAL.Abstract;

namespace DAL.Concrete
{
    public class FileRepository : IFileRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileRepository(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public string UploadFile(string containerName, string fileName, Stream fileContent)
        {
            try
            {
                var containerClient = GetContainer(containerName);

                var blobClient = containerClient.GetBlobClient(fileName);

                blobClient.Upload(fileContent, overwrite: true);

                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        public Stream GetFile(string containerName, string fileName)
        {
            try
            {
                var containerClient = GetContainer(containerName);

                var blobClient = containerClient.GetBlobClient(fileName);

                var response = blobClient.Download();
                return response.Value.Content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
                throw;
            }
        }

        public void DeleteFile(string containerName, string fileName)
        {
            try
            {
                var containerClient = GetContainer(containerName);

                var blobClient = containerClient.GetBlobClient(fileName);

                blobClient.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                throw;
            }
        }

        private BlobContainerClient GetContainer(string containerName)
        {
            var _containerName = containerName;

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();
            containerClient.SetAccessPolicy(PublicAccessType.Blob);

            return containerClient;
        }
    }
}