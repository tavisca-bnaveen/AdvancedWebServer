using System.Collections.Generic;
using System.Net;
using System;
namespace AdvancedWebServer
{
    public class HttpAppHandler
    {
        HttpListenerContext context;
        string filename;
        string domain;
        public HttpAppHandler(HttpListenerContext context)
        {
            this.context = context;
        }
        public void ParseUrl()
        {
            var URL = context.Request.Url;
            var uriPath = URL.AbsoluteUri;
            filename = context.Request.RawUrl;
            domain = uriPath.Remove(uriPath.Length - filename.Length+1, filename.Length-1);
            filename = context.Request.RawUrl.Remove(0, 1);
            //Console.WriteLine(uriPath);
        }
        public string GetDomain()
        {
            return domain;
        }
        public string GetFilename()
        {
            return filename;
        }
    }
}
