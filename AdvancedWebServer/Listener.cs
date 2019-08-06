using System;
using System.Net;
namespace AdvancedWebServer
{
    public class Listener
    {
        
        HttpListenerContext context;
        HttpListener web;
        ThreadListenerQueue queue;
        DomainPathStorage AllDomainsAndPaths;
        public Listener(ThreadListenerQueue queue, DomainPathStorage AllDomainsAndPaths)
        {
            this.queue = queue;
            this.AllDomainsAndPaths = AllDomainsAndPaths;
        }
        public void Start()
        {
           
                web = new HttpListener();

                //web.Prefixes.Add("http://localhost:8080/");
                //web.Prefixes.Add("http://localhost:3030/");
                foreach (var key in (AllDomainsAndPaths.GetDomainsAndPaths()).Keys)
                {
                web.Prefixes.Add(key);
                }
                Console.WriteLine("Listening..");

                web.Start();
            while (true)
            {
                context = web.GetContext();
                if(context!=null)
                    queue.Enqueue(context);
            }
                
                        
        }

    }
}
