namespace WebApi.OAuth2
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Routing;
    
    /// <summary>
    /// For APIs that are both resource servers and authorization servers
    /// The Global.asax.cs of your web application should extend this base class
    /// </summary>
    public class OAuthApiApplication : HttpApplication
    {
        public static readonly string OAUTH_ROUTE_PATTERN = "oauth";

        protected virtual void Application_Start()
        {
            // Add the OAuth Authorization handler to validate all requests
            GlobalConfiguration.Configuration.MessageHandlers.Add(new OAuthAuthorizationHandler());
            
            // remove xml formatter. json only
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            RouteTable.Routes.MapHttpRoute(
            name: "oauth",
            routeTemplate: OAUTH_ROUTE_PATTERN,
            defaults: new { controller = "OAuth"}
            );
        }
    }
}
