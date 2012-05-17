using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.OAuth2.Model
{
    public class AccessTokenRequest
    {
        /// <summary>
        /// your app id
        /// </summary>
        public string app_id { get; set; }
        
        /// <summary>
        /// Secret issued to your app
        /// </summary>
        public string app_secret{ get; set; }
        
        /// <summary>
        /// refresh_token
        /// authorization_code (currently not supported)
        /// </summary>
        public string grant_type { get; set; }
    }
}
