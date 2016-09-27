using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// The following using statements were added for this sample
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Notifications;
using Microsoft.IdentityModel.Protocols;
using System.Web.Mvc;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Threading;
using System.Globalization;
using System.IO;

namespace DH.ECAS_PoC
{
	public partial class Startup
	{
        // App config settings
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        // B2C policy identifiers
        public static string SignUpPolicyId = ConfigurationManager.AppSettings["ida:SignUpPolicyId"];
        public static string SignInPolicyId = ConfigurationManager.AppSettings["ida:SignInPolicyId"];
        public static string ProfilePolicyId = ConfigurationManager.AppSettings["ida:UserProfilePolicyId"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            // Configure OpenID Connect middleware for each policy
            app.UseOpenIdConnectAuthentication(CreateOptionsFromPolicy(SignUpPolicyId));
            app.UseOpenIdConnectAuthentication(CreateOptionsFromPolicy(ProfilePolicyId));
            app.UseOpenIdConnectAuthentication(CreateOptionsFromPolicy(SignInPolicyId));
        }

        // Used for avoiding yellow-screen-of-death
        private Task AuthenticationFailedNotification(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            notification.HandleResponse();
            if(notification.Exception.Message == "access_denied")
            {
                notification.Response.Redirect("/");
            }
            else
            {
                notification.Response.Redirect("/Home/Error?message=" + notification.Exception.Message);
            }

            return Task.FromResult(0);
        }

        private OpenIdConnectAuthenticationOptions CreateOptionsFromPolicy(string policy)
        {
            return new OpenIdConnectAuthenticationOptions
            {
                MetadataAddress = String.Format(aadInstance, tenant, policy),
                AuthenticationType = policy,
                
                ClientId = clientId,
                RedirectUri = redirectUri,
                PostLogoutRedirectUri = redirectUri,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthenticationFailed = AuthenticationFailedNotification,
                    SecurityTokenReceived = SecurityTokenReceivedNotification,
                    SecurityTokenValidated = SecurityTokenValidatedNotification
                },
                Scope = "openid",
                ResponseType = "id_token",
                
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                },
            };
        }

        private Task SecurityTokenValidatedNotification(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            var token = notification.ProtocolMessage.IdToken;

            if (!string.IsNullOrEmpty(token))
            {
                notification.AuthenticationTicket.Identity.AddClaim(new System.Security.Claims.Claim("id_token", token));
            }
            return Task.FromResult(0);
        }

        private Task SecurityTokenReceivedNotification(SecurityTokenReceivedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            //notification.Response.Redirect($"/Home/Index?message={notification.ProtocolMessage.IdToken}");
            
            
            // Buffer the response
            //var stream = arg.Response.Body;
            //var buffer = new MemoryStream();

            //buffer.Seek(0, SeekOrigin.Begin);
            //var reader = new StreamReader(buffer);
            //string responseBody = await reader.ReadToEndAsync();

            //// Now, you can access response body.
            //var result = responseBody;

            //// You need to do this so that the response we buffered
            //// is flushed out to the client application.
            //buffer.Seek(0, SeekOrigin.Begin);
            //await buffer.CopyToAsync(stream);

            return Task.FromResult(0);
        }
    }
}