using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using GoogleDrive_FHIRDocumentReference.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Diagnostics;

namespace GoogleDrive_FHIRDocumentReference.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        static string ApplicationName = "Drive API .NET Quickstart";
        //private const string PathToServiceAccountKeyFile = @"D:\Jeshika\Other\Xiao_Laoshi\MVC_Web_Upload_Gdrive\MVC_Web_Upload_Gdrive\WebUploadtoGdrive\WebUploadtoGdrive\Json\credentials.json";
        //public static string mediaJson = @"D:\Jeshika\Other\Xiao_Laoshi\ConsoleTestDriveApi\ConsoleTestDriveApi\FHIR_Media.json";
        //public static string fhirUrl = "https://hapi.fhir.org/baseR4/Media/";
        //public static string folderName = "SLI_UploadImage";
        private const string ServiceAccountEmail = "driveuploadtest@testapikey-305109.iam.gserviceaccount.com";
        private const string UploadFileName = "D:\\Jeshika\\Research\\SLI_ManagementImages\\images\\images\\compressed\\macrodermoscopic_skinlesionmacrodermoscopic01.jpeg";
        private const string DirectoryId = "10krlloIS2i_2u_ewkdv3_1NqcpmWSL1w";



        public DriveService service;
        //public UserCredential credential;
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment Environment;
        public HomeController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, ILogger<HomeController> logger)
        {
            Environment = hostingEnvironment;
            Configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //using (var stream =new FileStream(Configuration["PathToServiceAccountKeyFile"], FileMode.Open, FileAccess.Read))
                //{

                //    /* The file token.json stores the user's access and refresh tokens, and is created
                //     automatically when the authorization flow completes for the first time. */
                //    string credPath = @"D:\Jeshika\Research\GoogleDrive_FHIRDocumentReference\GoogleDrive_FHIRDocumentReference\JSON\token.json";
                //    var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //        GoogleClientSecrets.FromStream(stream).Secrets,
                //        Scopes,
                //        "user",
                //        CancellationToken.None,
                //        new FileDataStore(credPath, true)).Result;
                //    Console.WriteLine("Credential file saved to: " + credPath);
                //}

                //// Load the Service account credentials and define the scope of its access.
                //var credential = GoogleCredential.FromFile(@"D:\Jeshika\Research\GoogleDrive_FHIRDocumentReference\GoogleDrive_FHIRDocumentReference\Data\jsoncredentials.json")
                //                .CreateScoped(DriveService.ScopeConstants.Drive);

                //// Create the  Drive service.
                //var service = new DriveService(new BaseClientService.Initializer()
                //{
                //    HttpClientInitializer = credential
                //});
                //// Upload file Metadata
                //var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                //{
                //    Name = "Test hello uploaded.txt",
                //    Parents = new List<string>() { "10krlloIS2i_2u_ewkdv3_1NqcpmWSL1w" }
                //};
                //string uploadedFileId;
                //// Create a new file on Google Drive
                //await using (var fsSource = new FileStream(UploadFileName, FileMode.Open, FileAccess.Read))
                //{
                //    // Create a new file, with metadata and stream.
                //    var request = service.Files.Create(fileMetadata, fsSource, "text/plain");
                //    request.Fields = "*";
                //    var results = await request.UploadAsync(CancellationToken.None);

                //    if (results.Status == UploadStatus.Failed)
                //    {
                //        Console.WriteLine($"Error uploading file: {results.Exception.Message}");
                //    }

                //    // the file id of the new file we created
                //    uploadedFileId = request.ResponseBody?.Id;
                //}


                //using (var stream = new FileStream(Configuration["PathToServiceAccountKeyFile"], FileMode.Open, FileAccess.Read))
                //{
                //    /* The file token.json stores the user's access and refresh tokens, and is created
                //     automatically when the authorization flow completes for the first time. */
                //    string credPath = Configuration["credPath"];
                //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //        GoogleClientSecrets.FromStream(stream).Secrets,
                //        Scopes,
                //        "user",
                //        CancellationToken.None,
                //        new FileDataStore(credPath, true)).Result;
                //    Console.WriteLine("Credential file saved to: " + credPath);
                //}

                // Misalkan Anda telah mendapatkan nilai clientId, clientSecret, dan accessToken setelah login
                //string clientId = "YOUR_CLIENT_ID";
                //string clientSecret = "YOUR_CLIENT_SECRET";
                //string accessToken = "YOUR_ACCESS_TOKEN";

                //// Buat instance GoogleDriveService
                //var googleDriveService = new GoogleDriveService(clientId, clientSecret, accessToken);

                //// Misalkan Anda telah mendapatkan file gambar dalam bentuk byte array
                //string filePath = "image.jpg";
                ////string path = HttpContext.Current.Server.MapPath("~/image/noimage.jpg");
                ////byte[] imageBytes  = File.ReadAllBytes(fileName);
                //byte[] imageBytes = GetImageBytes(filePath); // Fungsi untuk mendapatkan byte array gambar

                //// Panggil metode UploadImage untuk mengunggah gambar
                //string fileId = googleDriveService.UploadImage(filePath, imageBytes);

                return View("UploadToGDrive");
            }
            else
            {
                //Page.Title = "Home page for guest user.";
                return View();
            }
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public byte[] GetImageBytes(string filePath)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
    }
}