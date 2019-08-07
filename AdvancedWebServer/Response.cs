using System.IO;
using System.Net;
namespace AdvancedWebServer
{
    public class Response
    {
        HttpListenerContext context;
        
        public Response(HttpListenerContext context)
        {
            this.context = context;
        }
        public void SendReponse(byte[] bytes)
        {
            if (bytes != null)
            {
                var response = context.Response;

                response.ContentLength64 = bytes.Length;
                Stream output = response.OutputStream;

                output.Write(bytes, 0, bytes.Length);
            }

        }
    }
}
