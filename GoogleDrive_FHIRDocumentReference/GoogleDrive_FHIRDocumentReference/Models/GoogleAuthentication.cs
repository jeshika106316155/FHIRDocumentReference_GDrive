﻿using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using NWebsec.AspNetCore.Core.Web;
using System.Web;
using NuGet.Packaging.Core;
using System.Security.Policy;

namespace GoogleDrive_FHIRDocumentReference.Models
{
    public class GoogleAuthentication
    {
        private static readonly string ClientId = "YOUR_CLIENT_ID";
        private static readonly string ClientSecret = "YOUR_CLIENT_SECRET";
        private static readonly string[] Scopes = { DriveService.Scope.Drive };

        public static string GetAccessToken()
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = ClientId,
                    ClientSecret = ClientSecret
                },
                Scopes = Scopes,
                DataStore = new FileDataStore("Drive.Api.Auth.Store")
            };

            //var flow = new GoogleAuthorizationCodeFlow(initializer);
            //var context = new HttpContextWrapper(HttpContext);

            //var codeReceiver = new AuthorizationCodeMvcApp(context, new AppFlowMetadata(flow, initializer));
            //var authCode = codeReceiver.FlowMetadata.GetAuthorizationCode(context.Request);

            //var token = flow.ExchangeCodeForTokenAsync("", authCode, "https://localhost:44300/Home/GoogleCallback", CancellationToken.None).Result;


            var flow = new GoogleAuthorizationCodeFlow(initializer);

            //var tokenResponse = await flow.ExchangeCodeForTokenAsync("user", code, Url.Action("GoogleCallback", "Account", null, Request.Scheme));

            //var accessToken = tokenResponse.AccessToken;
            //var refreshToken = tokenResponse.RefreshToken;

            return null;//token.AccessToken;
        }
    }
}
