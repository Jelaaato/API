using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using backendapi.Providers;
using System.Data.Entity;
using backendapi.Models;

[assembly: OwinStartup(typeof(backendapi.Startup))]

namespace backendapi    
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.;
            
            ConfigureOAuth(app);
          
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, backendapi.Migrations.Configuration>());
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
           
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new RefreshTokenProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}
