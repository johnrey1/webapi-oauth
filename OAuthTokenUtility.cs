namespace WebApi.OAuth2
{
    using System;
    using System.Linq;
    using System.Net.Http;

    public class OAuthTokenUtility
    {

        public static bool ValidateAccessToken(string accessToken)
        {
            using (AuthorizationDataContext oauthDataCtxt = new AuthorizationDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["OAuthDb"].ConnectionString))
            {
                var appAuth = (
                                from appAuths in oauthDataCtxt.AppAuthorizations
                                where appAuths.AuthToken == accessToken && (!appAuths.AuthTokenExpiration.HasValue || appAuths.AuthTokenExpiration.Value <= DateTime.UtcNow)
                                select appAuths
                              ).FirstOrDefault();

                return appAuth != null;
            }
        }

        public static bool AuthorizeRequest(HttpRequestMessage request, string accessToken)
        {
            return false;
        }

        public static AppAuthorization ValidateRefreshToken(string appId, string appSecret, string refreshToken)
        {
            AuthorizationDataContext oauthDataCtxt = new AuthorizationDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["OAuthDb"].ConnectionString);

            var appAuth = from apps in oauthDataCtxt.Applications
                          where apps.AppPublicId == appId
                              && apps.AppSecret == appSecret
                          select (
                                  from authorizations in oauthDataCtxt.AppAuthorizations
                                  where authorizations.RefreshToken == refreshToken
                                          && (authorizations.RefreshTokenExpiration == null || authorizations.RefreshTokenExpiration < DateTime.UtcNow)
                                  select authorizations
                                  ).FirstOrDefault();

            return appAuth as AppAuthorization;
        }

        /// <summary>
        /// Refreshes an access token by expiring the existing tokens and creating a new appauthorization entry
        /// TODO: allow expiration dates
        /// </summary>
        /// <param name="appAuth"></param>
        /// <returns></returns>
        public static AppAuthorization AccessTokenRefresh(AppAuthorization appAuth)
        {
            // expire previous token
            using(AuthorizationDataContext oauthDataCtxt = new AuthorizationDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["OAuthDb"].ConnectionString))
            {
                /// expire old auth
                var oldAppAuth = oauthDataCtxt.AppAuthorizations.First( a => a.Id == appAuth.Id);
                oldAppAuth.AuthTokenExpiration = DateTime.UtcNow;
                oldAppAuth.RefreshTokenExpiration = DateTime.UtcNow;
                
                // create new auth
                var newAppAuth = new AppAuthorization()
                {
                    AppId = appAuth.AppId,
                    UserId = appAuth.UserId,
                    Scope = appAuth.Scope,
                    AuthToken = GenerateToken(),
                    RefreshToken = GenerateToken(),
                    Created = DateTime.UtcNow
                };

                oauthDataCtxt.AppAuthorizations.InsertOnSubmit(newAppAuth);

                oauthDataCtxt.SubmitChanges();

                return newAppAuth as AppAuthorization;
            }
        }

        /// <summary>
        /// create a random string, sha256 hash it, and return the base 64 encoded representation
        /// TODO: change generate token into strategy pattern and allow injection on global asax startup
        /// </summary>
        /// <returns></returns>
        private static string GenerateToken()
        {
            Random pseudoGen = new Random();

            byte[] randomBytes = new byte[2048];

            pseudoGen.NextBytes(randomBytes);

            byte[] shaHash = System.Security.Cryptography.SHA256.Create().ComputeHash(randomBytes);

            return Convert.ToBase64String(shaHash);
        }
    }
}
