using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AdvancedWebServer
{
    public class PostHandler
    {
        JObject jObject;
        HttpListenerContext httpListenerContext;
        public PostHandler(HttpListenerContext httpListenerContext)
        {
            this.httpListenerContext = httpListenerContext;
            string text;
            using (var reader = new StreamReader(httpListenerContext.Request.InputStream,
                         httpListenerContext.Request.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }
            jObject = JObject.Parse(text);
        }
        public string GetParameter()
        {
            return jObject["year"].ToString();
        }
        public byte[] ConvertJsonObject(bool answer)
        {
            jObject["year"] = answer;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            return bytes;
        }
    }
}
