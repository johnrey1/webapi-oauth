namespace WebApi.OAuth2.Model
{
    using System;

    public class AccessTokenMessage
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime? expiration_utc { get; set; }
        public string token_type { get; set; }
    }
}
