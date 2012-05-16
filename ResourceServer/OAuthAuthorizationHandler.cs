namespace WebApi.OAuth2
{
    using System;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading.Tasks;

    /// <summary>
    /// A WebApi Message handler to check authorization on incoming web requests
    /// based on the OAuthAthorizationManger from the 
    /// </summary>
    public class OAuthAuthorizationHandler : DelegatingHandler
    {
        public OAuthAuthorizationHandler()
        {
        }

        /// <summary>
        /// Check if user is authenticated
        /// If user is authenticated, check if user is authorized to perform action on requested resource
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            

            try
            {
                var tcs = new TaskCompletionSource<HttpResponseMessage>();
                
                if (!IsSsl(request))
                {                    
                    tcs.SetResult(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
                    return tcs.Task;        
                }

                if (!IsValidAccessToken(request))
                {
                    // return 401 error
                    tcs.SetResult(new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
                    return tcs.Task;
                }

                if (!IsTokenAuthorized(request))
                {
                    // return 401 error
                    tcs.SetResult(new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden));
                    return tcs.Task;
                }

            }
            catch (Exception ex)
            {
                cancellationToken = new System.Threading.CancellationToken(true);
            }

            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// this should validate the access token against the requesturi and method it is attempting to execute
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private static bool IsValidAccessToken(HttpRequestMessage request)
        {
            var accessToken = request.RequestUri.ParseQueryString()["access_token"];

            // verify token exists and is not expired
            
            return accessToken == "asdf";
        }

        private static bool IsTokenAuthorized(HttpRequestMessage request)
        {
            // get role by access token
            // get ownerid by access token
            // get list of roles by access token
            // validate that request / action is contained within role
            // get list of roles by owner
            // validate that roles of access token are still valid for the owner ( should we invalidate all tokens when a users role changes?)
            return true;
        }

        private static bool IsSsl(HttpRequestMessage request)
        {
            if (request.RequestUri.Scheme.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
