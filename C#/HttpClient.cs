using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace Example
{
    public class HttpClientResponse
    {
        public String responseBody {get; set;}
        public HttpStatusCode statusCode { get; set; }
        
        public HttpClientResponse()
        {
        } 
    }

    public class HttpClient
    {
        private const string Method = "GET";
        private const string Method1 = "POST";
        private const string AGENT = "Custom";
        // Windows default Code Page
        private const int EncondignCode = 1252;
        private const int TIMEOUT = 60000;

        public static HttpClientResponse doGet(string url)
        {
            return doRequest(url, Method);
        }

        public static HttpClientResponse doPost(string url)
        {
            return doRequest(url, Method1);
        }

        private static HttpClientResponse doRequest(string url, string method)
        {
            HttpClientResponse result = new HttpClientResponse();
            HttpWebResponse loWebResponse = null;
            StreamReader loResponseStream = null;

            try
            {
                HttpWebRequest loHttp = (HttpWebRequest)WebRequest.Create(url);

                loHttp.Method = method;
                loHttp.Timeout = TIMEOUT;
                loHttp.UserAgent = AGENT;              

                loWebResponse = (HttpWebResponse)loHttp.GetResponse();
                result.statusCode = loWebResponse.StatusCode;

                Encoding enc = Encoding.GetEncoding(EncondignCode);

                loResponseStream = new StreamReader(loWebResponse.GetResponseStream(), enc);
                String body = loResponseStream.ReadToEnd();
                result.responseBody = body;
            }
            catch
            {

            }
            finally
            {
                try
                {
                    loWebResponse.Close();
                    loResponseStream.Close();
                }
                catch
                {
                }
            }

            return result;
        }
    }
}
