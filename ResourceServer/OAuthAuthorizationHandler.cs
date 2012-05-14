namespace WebApi.OAuth2
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IdentityModel.Policy;
    using System.Net.Http;
    using System.Security.Principal;
    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OAuth2;
using System.Net.Http.Headers;
    using System.Collections;
    using System.Text;

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
            // Check if user is authenticated...
            try
            {
                var principal = VerifyOAuth2(request);
                                
                if (principal != null)
                {
                    var policy = new OAuthPrincipalAuthorizationPolicy(principal);
                    var policies = new List<IAuthorizationPolicy> {
						policy,
					};

                    var securityContext = new ServiceSecurityContext(policies.AsReadOnly());
                    if (operationContext.IncomingMessageProperties.Security != null)
                    {
                        operationContext.IncomingMessageProperties.Security.ServiceSecurityContext = securityContext;
                    }
                    else
                    {
                        operationContext.IncomingMessageProperties.Security = new SecurityMessageProperty
                        {
                            ServiceSecurityContext = securityContext,
                        };
                    }

                    securityContext.AuthorizationContext.Properties["Identities"] = new List<IIdentity> {
						principal.Identity,
					};

                    // Only allow this method call if the access token scope permits it.
                    return principal.IsInRole();
                }
                else
                {
                    return false;
                }
            }
            catch (ProtocolException ex)
            {
                //Global.Logger.Error("Error processing OAuth messages.", ex);
            }

            return false;
        }



        private static IPrincipal VerifyOAuth2(HttpRequestMessage request)
        {
            // for this sample where the auth server and resource server are the same site,
            // we use the same public/private key.
            using (var signing = EncryptionConfig.CreateAuthorizationServerSigningServiceProvider())
            {
                using (var encrypting = EncryptionConfig.CreateResourceServerEncryptionServiceProvider())
                {
                    var resourceServer = new ResourceServer(new StandardAccessTokenAnalyzer(signing, encrypting));

                    IPrincipal result;

                    

                    var error = resourceServer.VerifyAccess(
                                                        HttpRequestInfo.Create( request.Method.Method, 
                                                                                request.RequestUri, 
                                                                                null, 
                                                                                request.Headers.ToNameValueCollection()),
                                                        out result);

                    // TODO: return the prepared error code.
                    return error != null ? null : result;
                }
            }
        }

    }
}
