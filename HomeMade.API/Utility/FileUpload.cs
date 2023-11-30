using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HomeMade.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMade.Api.Utility
{
    public static class FileUpload
    {
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<string> UploadToStorage(IFormFile image, AzureStorageConfig _storageConfig)
        {
            Uri fileUrl = new Uri(_storageConfig.ImageUrlBaseAddress + "/" + image.FileName);

            if (IsImage(image))
            {
                if (image.Length > 0)
                {
                    using (Stream stream = image.OpenReadStream())
                    {
                        // Create StorageSharedKeyCredentials object by reading
                        // the values from the configuration (appsettings.json)
                        StorageSharedKeyCredential storageCredentials =
                            new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

                        // Create the blob client.
                        BlobClient blobClient = new BlobClient(fileUrl, storageCredentials);

                        // Upload the file
                        await blobClient.UploadAsync(stream);

                        return fileUrl.ToString();
                    }
                }
            }

            return null;
        }
    }
}
