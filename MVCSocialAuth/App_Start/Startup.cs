using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using Microsoft.Owin;
using System.Security.Claims;

[assembly: OwinStartup(typeof(MVCSocialAuth.App_Start.Startup))]
namespace MVCSocialAuth.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                
            });

            // MAKE SURE PROVIDERS KEYS ARE SET IN the CodePasteKeys.json FILE
         

            // these values are stored in CodePasteKeys.json
            // and are NOT included in repro - autocreated on first load
         
                //app.UseGoogleAuthentication(
                //    clientId: "20735364608-anf3cg3m2pau38rgd40nehq2afb0kine.apps.googleusercontent.com ",
                //    clientSecret: "M6i0ixhZQWPCJ5MCwjdsfJhX");
           

            //if (!string.IsNullOrEmpty(App.Secrets.TwitterConsumerKey))
            //{
            //    app.UseTwitterAuthentication(
            //        consumerKey: App.Secrets.TwitterConsumerKey,
            //        consumerSecret: App.Secrets.TwitterConsumerSecret);
            //}

            //if (!string.IsNullOrEmpty(App.Secrets.GitHubClientId))
            //{
            //    app.UseGitHubAuthentication(App.Secrets.GitHubClientId, App.Secrets.GitHubClientSecret);
            //}

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}