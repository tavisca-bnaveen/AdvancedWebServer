using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AdvancedWebServer
{
    public class PostHandler
    {
       public JObject jObject;
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
       
        public byte[] ConvertJsonObject(JObject Value)
        {
            
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Value);
            byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            return bytes;
        }
    }
}
