namespace WebApi.OAuth2
{
    using System;
    using System.Security.Cryptography;
    using System.Web;
    using System.Web.Http;
    
    /// <summary>
    /// For APIs that are both resource servers and authorization servers
    /// The Global.asax.cs of your web application should extend this base class
    /// </summary>
    public class OAuthApiApplication : HttpApplication
    {


        protected virtual void Application_Start()
        {
            CreateOAuthDb();
            
            // Add the OAuth Authorization handler to validate all requests
            GlobalConfiguration.Configuration.MessageHandlers.Add(new OAuthAuthorizationHandler());
        }

        private void CreateOAuthDb()
        {
            // Configure the auth database
            AuthorizationDataContext oauthDataCtxt = new AuthorizationDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["OAuthDb"].ConnectionString);
            if (oauthDataCtxt.DatabaseExists())
            {
                Console.WriteLine("database already exists!");
            }
            else
            {
                oauthDataCtxt.CreateDatabase();
            }
        }


    }
}
