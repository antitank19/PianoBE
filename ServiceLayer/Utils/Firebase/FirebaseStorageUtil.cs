using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Utils
{
    public static class FirebaseStorageUtil
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public static async Task<string> UploadFileAsync(string localPath, string destinationPath, string bucket, string authToken = null)
        {
            FirebaseStorage firebaseStorage = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = authToken != null ? () => Task.FromResult(authToken) : (Func<Task<string>>)null,
                    ThrowOnCancel = true
                });
            var cancellation = new System.Threading.CancellationTokenSource();

            using var fileStream = File.Open(localPath, FileMode.Open);

            var task = firebaseStorage
                .Child(destinationPath)
                .PutAsync(fileStream, cancellation.Token);

            try
            {
                string downloadUrl = await task;
                return downloadUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public static async Task<string> UploadFileAsync(IFormFile file, string destinationPath, string bucket, string authToken = null)
        {
            FirebaseStorage firebaseStorage = new FirebaseStorage(
               bucket,
               new FirebaseStorageOptions
               {
                   AuthTokenAsyncFactory = authToken != null ? () => Task.FromResult(authToken) : (Func<Task<string>>)null,
                   ThrowOnCancel = true
               });
            //var cancellation = new System.Threading.CancellationTokenSource();
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            using var fileStream = file.OpenReadStream();


            try
            {
                string downloadUrl = await firebaseStorage
                    .Child(destinationPath)
                    .Child(uniqueFileName)
                    .PutAsync(fileStream);
                return downloadUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }
    }

}
