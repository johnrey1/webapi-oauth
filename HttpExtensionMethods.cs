

namespace WebApi.OAuth2
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net.Http.Headers;
    using System.Text;

    public static class HttpExtensionMethods
    {
        public static NameValueCollection ToNameValueCollection(this HttpRequestHeaders headers)
        {
            NameValueCollection headerCollection = new NameValueCollection();

            StringBuilder headerValues = new StringBuilder();
            foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
            {
                foreach (string value in header.Value)
                {
                    headerValues.Append(value).Append(",");
                }

                if (headerValues.Length > 0)
                {
                    // remove trailing comma
                    headerValues.Remove(headerValues.Length - 1, 1);
                }

                headerCollection.Add(header.Key, headerValues.ToString());
                headerValues.Clear();
            }

            return headerCollection;
        }
    }
}
