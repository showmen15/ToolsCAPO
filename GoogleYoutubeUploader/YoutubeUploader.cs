using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace GoogleYoutubeUploader
{
    public class YoutubeUploader
    {
        private UserCredential credential;

        //private string googleCredentialLocationPath;

        private string sFileID;

        public YoutubeUploader()
        {
            //this.googleCredentialLocationPath = googleCredentialLocationPath;
        }

        public string YoutubeUpload(string filePath, string Title, string Description, string[] Tags)
        {
            Task temp = run(filePath, Title, Description, Tags);
            temp.Wait();

           // new .Wait();

            return sFileID;
        }

        private async Task run(string filePath, string Title, string Description, string[] Tags)
        {
            sFileID = string.Empty;

            UserCredential credential;

                using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
                {
                    credential = await  GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        // This OAuth 2.0 access scope allows an application to upload files to the
                        // authenticated user's YouTube channel, but doesn't allow other types of access.
                        new[] { YouTubeService.Scope.YoutubeUpload },
                        "user",
                        CancellationToken.None
                    );
                }

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
                });

                var video = new Video();
                video.Snippet = new VideoSnippet();
                video.Snippet.Title = Title; // "Default Video Title";
                video.Snippet.Description = Description; // "Default Video Description";

                if (Tags != null)
                    video.Snippet.Tags = Tags; //new string[] { "tag1", "tag2" };

                video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
                video.Status = new VideoStatus();
                video.Status.PrivacyStatus = "unlisted"; // or "private" or "public"
                                                         //var filePath = @"C:\git\youtube\Test.mp4"; //@"REPLACE_ME.mp4"; // Replace with path to actual movie file.

                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                    videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                 var tt =  videosInsertRequest.Upload();//videosInsertRequest.UploadAsync();

              //  sFileID = video.Id;
            }
        }

        private void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        private void videosInsertRequest_ResponseReceived(Video video)
        {
            sFileID = video.Id;

            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
        }
    }
}
