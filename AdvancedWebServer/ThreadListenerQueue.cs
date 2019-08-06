using System.Collections.Generic;
using System.Net;
namespace AdvancedWebServer
{
    public class ThreadListenerQueue
    {
        HttpListenerContext context;
        List<HttpListenerContext> listeners = new List<HttpListenerContext>();
        public void Enqueue(HttpListenerContext context)
        {
            listeners.Add(context);
        }
        public HttpListenerContext Dequeue()
        {
            
                context = listeners[0];
                listeners.RemoveAt(0);
                return context;
           
        }
        public List<HttpListenerContext> GetQueue()
        {
            return listeners;
        }
    }
}
