namespace WebApi.OAuth2 {

    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using WebApi.OAuth2.Model;

	public class OAuthController : ApiController {

        public static readonly string GRANT_REFRESH = "refresh_token";
        public static readonly string GRANT_ACCESS = "access_token";

		/// <summary>
		/// The OAuth 2.0 token endpoint.
		/// </summary>
		/// <returns>The response to the Client.</returns>
		public string Post(AccessTokenRequest tokenRequest) {

            // only 'refresh' is implemented
            if (tokenRequest.grant_type == GRANT_REFRESH)
            {
                string refreshToken = this.Request.RequestUri.ParseQueryString()["refresh_token"];

                AppAuthorization appAuth = OAuthTokenUtility.ValidateRefreshToken(tokenRequest.app_id, tokenRequest.app_secret, refreshToken);

                if (appAuth == null)
                {
                    // invalid request
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorMessage() { Type = "OAuthException", Message = "could not grant refreshed access token. please check your client id, client secret, and refresh token id (did it expire?)" });
                }
                else
                {
                    // create new access token
                    AppAuthorization newAppAuth = OAuthTokenUtility.AccessTokenRefresh(appAuth);

                    return Newtonsoft.Json.JsonConvert.SerializeObject(new AccessTokenMessage()
                    {
                        access_token = newAppAuth.AuthToken,
                        refresh_token = newAppAuth.RefreshToken,
                        token_type = "bearer",
                        expiration_utc = newAppAuth.AuthTokenExpiration,
                        scope = newAppAuth.Scope
                    });
                }
            }

            if (tokenRequest.grant_type == GRANT_ACCESS)
            {
                // requesting an authorization token using a short lived auth code
                var authCode = this.Request.RequestUri.ParseQueryString()["code"];

                return Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorMessage() { Type = "OAuthException", Message = "new access tokens not granted by this server" }); 
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorMessage() { Type = "OAuthException", Message = "not a valid grant_type" });
		}
    }
}
