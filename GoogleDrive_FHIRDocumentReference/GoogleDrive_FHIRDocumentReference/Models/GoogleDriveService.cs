using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2.Responses;
using System.Net.Mime;

namespace GoogleDrive_FHIRDocumentReference.Models
{
    public class GoogleDriveService
    {
        private readonly DriveService _driveService;

        public GoogleDriveService(string clientId, string clientSecret, TokenResponse accessToken)
        {
            string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
            var credential = new UserCredential(new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    }
                }),
                "user", accessToken);

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        }

        public string UploadImage(string fileName, byte[] imageBytes)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                MimeType = "image/jpeg" // Ubah sesuai tipe MIME gambar yang diunggah
            };

            FilesResource.CreateMediaUpload request = (FilesResource.CreateMediaUpload)_driveService.Files.Create(fileMetadata, new MemoryStream(imageBytes), "image/jpeg").Upload();
            request.Fields = "*";
            var results = request.Upload();
            
           //request.ProgressChanged += (IApiUploadProgress progress) =>
           // {
           //     // Lakukan sesuatu dengan kemajuan unggahan jika diperlukan
           // };           //request.ProgressChanged += (IApiUploadProgress progress) =>
           // {
           //     // Lakukan sesuatu dengan kemajuan unggahan jika diperlukan
           // };

            request.ResponseReceived += (Google.Apis.Drive.v3.Data.File file) =>
            {
                // Lakukan sesuatu setelah unggahan berhasil, misalnya dapatkan ID file yang diunggah
                string fileId = file.Id;
                // Lakukan operasi lain yang diperlukan
                if (results.Status == Google.Apis.Upload.UploadStatus.Failed) // == UploadStatus.Failed
                {
                    Console.WriteLine($"Error uploading file: {results.Exception.Message}");
                    var reqMessage = results.Exception.Message;
                }
                else
                { }
            };

            request.Upload();

            return request.ResponseBody?.Id;
        }
    }
}
