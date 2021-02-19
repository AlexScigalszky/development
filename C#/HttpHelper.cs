using System.Web;

namespace Examples.Helpers
{
    public class HttpHelper
    {
        private const string HTTP_X_FORWARDED_FOR = "HTTP_X_FORWARDED_FOR";
        private const string REMOTE_ADDR = "REMOTE_ADDR";

        public static string SafeUrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string SafeUrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        public static string GetClientIPAddress()
        {
            var visitorIPAddress = HttpContext.Current.Request.ServerVariables[HTTP_X_FORWARDED_FOR];

            if (string.IsNullOrEmpty(visitorIPAddress))
            {
                visitorIPAddress = HttpContext.Current.Request.ServerVariables[REMOTE_ADDR];
            }

            if (string.IsNullOrEmpty(visitorIPAddress))
            {
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;
            }

            return visitorIPAddress;
        }
    }
}