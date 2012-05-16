namespace WebApi.OAuth2 {
	using System;
    using System.Net;
    using System.Web;
	using System.Web.Mvc;


	public class OAuthController : Controller {
		
		/// <summary>
		/// The OAuth 2.0 token endpoint.
		/// </summary>
		/// <returns>The response to the Client.</returns>
		public ActionResult Token() {
            throw new NotImplementedException();
		}

		/// <summary>
		/// Prompts the user to authorize a client to access the user's private data.
		/// </summary>
		/// <returns>The browser HTML response that prompts the user to authorize the client.</returns>
		[Authorize, AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
		
		public ActionResult Authorize() {
            throw new NotImplementedException();
		}

		/// <summary>
		/// Processes the user's response as to whether to authorize a Client to access his/her private data.
		/// </summary>
		/// <param name="isApproved">if set to <c>true</c>, the user has authorized the Client; <c>false</c> otherwise.</param>
		/// <returns>HTML response that redirects the browser to the Client.</returns>
		[Authorize, HttpPost, ValidateAntiForgeryToken]
		public ActionResult AuthorizeResponse(bool isApproved) {
            throw new NotImplementedException();
		}
	}
}
