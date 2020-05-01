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
using System.Collections.Generic;

namespace GoogleYoutubeUploader
{
    public class YoutubeUploader
    {
        //https://developers.google.com/youtube/v3/quickstart/go
        //https://developers.google.com/youtube/v3/guides/authentication
        //https://developers.google.com/youtube/v3/guides/auth/installed-apps
        //https://developers.google.com/youtube/v3/docs/videos/list?apix_params=%7B%22part%22%3A%22snippet%2CcontentDetails%22%2C%22id%22%3A%226cmomowk9q8%22%7D
        //https://developers.google.com/youtube/v3/docs/videos/update?apix_params=%7B%22part%22%3A%22snippet%2CcontentDetails%22%2C%22resource%22%3A%7B%22id%22%3A%226cmomowk9q8%22%2C%22snippet%22%3A%7B%22title%22%3A%22Test%2025%22%2C%22categoryId%22%3A%2222%22%7D%7D%7D

        private UserCredential credential;
        private YouTubeService youtubeService;

        //private string googleCredentialLocationPath;

        private string sFileID;

        public YoutubeUploader()
        {
            //this.googleCredentialLocationPath = googleCredentialLocationPath;

            credential = initUserCredential("client_secrets.json").Result;

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

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
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
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
            video.Status.PrivacyStatus = "public"; // or "private" or "public" or "unlisted"
                                                   //var filePath = @"C:\git\youtube\Test.mp4"; //@"REPLACE_ME.mp4"; // Replace with path to actual movie file.

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                var tt = videosInsertRequest.Upload();//videosInsertRequest.UploadAsync();

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


        public string[] YoutubeGetList()
        {
            Task temp = runGetMovieList();
            temp.Wait();



            return null;
        }


        private async Task runGetMovieList()
        {

            UserCredential credential = initUserCredential("client_secrets.json").Result;





            //using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            //{
            //    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        // This OAuth 2.0 access scope allows an application to upload files to the
            //        // authenticated user's YouTube channel, but doesn't allow other types of access.
            //        new[] { YouTubeService.Scope.YoutubeReadonly },
            //        "user",
            //        CancellationToken.None
            //    );
            //}

            //var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            //});

            //var temp = youtubeService.Activities.List("snippet,contentDetails");

            //temp.ChannelId = "UCcmuUBdIM1P4dpHnelvXN4g";

            //var channelsListResponse = await temp.ExecuteAsync(); ;


            //ChannelsResource.ListRequest channelsListRequest = youtubeService.Channels.List("snippet,contentDetails");




            //channelsListRequest.MaxResults = 20;



            //channelsListRequest.HttpMethod;

            //List <string> videoIdList = new List<string>();
            //List<Video> videoList = new List<Video>();
            //string uploadsListId = null;

            //try
            //{
            //    ChannelListResponse channelsListResponse = await channelsListRequest.ExecuteAsync();
            //    uploadsListId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;
            //}
            //catch (Exception ex)
            //{
            //    //ErrorException = ex;
            //   // return videoList;
            //}




            //var channelsListRequest = youtubeService.Channels.List("contentDetails");
            ////channelsListRequest.Mine = true;

            //// Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            //var channelsListResponse = await channelsListRequest.ExecuteAsync();

            //foreach (var channel in channelsListResponse.Items)
            //{
            //    // From the API response, extract the playlist ID that identifies the list
            //    // of videos uploaded to the authenticated user's channel.
            //    var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;

            //    Console.WriteLine("Videos in list {0}", uploadsListId);

            //    var nextPageToken = "";
            //    while (nextPageToken != null)
            //    {
            //        var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
            //        playlistItemsListRequest.PlaylistId = uploadsListId;
            //        playlistItemsListRequest.MaxResults = 50;
            //        playlistItemsListRequest.PageToken = nextPageToken;

            //        // Retrieve the list of videos uploaded to the authenticated user's channel.
            //        var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

            //        foreach (var playlistItem in playlistItemsListResponse.Items)
            //        {
            //            // Print information about each video.
            //            Console.WriteLine("{0} ({1})", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId);
            //        }

            //        nextPageToken = playlistItemsListResponse.NextPageToken;
            //    }
            //}





        }


        public void UpdateVideoTitel(string Id,string Title)
        {            
            Video v = new Video();
            v.Id = Id;
            v.Snippet = new VideoSnippet();
            v.Snippet.CategoryId = "22";
            v.Snippet.Title = Title;

            var channelsListRequest = youtubeService.Videos.Update(v, "snippet,contentDetails");

            var temp = channelsListRequest.Execute();
        }

        public string GetVideoTitel(string sId)
        {                      
            var channelsListRequest = youtubeService.Videos.List("snippet,contentDetails");
            channelsListRequest.Id = sId;

            var temp = channelsListRequest.Execute();

            return temp.Items[0].Snippet.Localized.Title;
        }

        private async Task<UserCredential> initUserCredential(string sPathSecrets)
        {
            UserCredential credential;

            using (var stream = new FileStream(sPathSecrets, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.YoutubeForceSsl },
                    "user",
                    CancellationToken.None
                );
            }

            return credential;
        }
    }
}
