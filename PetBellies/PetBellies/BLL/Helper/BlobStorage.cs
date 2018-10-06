using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetBellies.BLL.Helper
{
    public class BlobStorage
    {
        private CloudBlobContainer invmeContainer;
        private const string CONTAINER_REF = "appmancs";

        public async Task<List<Uri>> GetAllBlobUrisAsync()
        {
            CloudBlobClient cloudBlobClient = CloudStorageAccount
               .Parse(GlobalVariables.AzureBlobStorageConnectionString)
               .CreateCloudBlobClient();

            invmeContainer = cloudBlobClient.GetContainerReference(CONTAINER_REF);

            var containerToken = new BlobContinuationToken();
            var allBlobs = await invmeContainer.ListBlobsSegmentedAsync(containerToken).ConfigureAwait(false);

            var uris = allBlobs.Results.Select(b => b.Uri).ToList();

            return uris;
        }

        public async Task<string> UploadFileAsync(string filepath, Stream file)
        {
            CloudBlobClient cloudBlobClient = CloudStorageAccount
               .Parse(GlobalVariables.AzureDBConnectionString)
               .CreateCloudBlobClient();

            invmeContainer = cloudBlobClient.GetContainerReference(CONTAINER_REF);

            var uniqueBlobName = Guid.NewGuid().ToString();
            uniqueBlobName += Path.GetExtension(filepath);

            var blobRef = invmeContainer.GetBlockBlobReference(uniqueBlobName);

            await blobRef.UploadFromStreamAsync(file).ConfigureAwait(false);

            return uniqueBlobName;
        }

        public async Task<bool> DeleteFileAsync(string name)
        {
            CloudBlobClient cloudBlobClient = CloudStorageAccount
               .Parse(GlobalVariables.AzureDBConnectionString)
               .CreateCloudBlobClient();

            invmeContainer = cloudBlobClient.GetContainerReference(CONTAINER_REF);
            var blob = invmeContainer.GetBlobReference(name);
            return await blob.DeleteIfExistsAsync();
        }
    }
}