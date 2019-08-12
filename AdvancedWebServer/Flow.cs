using System;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdvancedWebServer
{
    public class Flow
    {
        ThreadListenerQueue queue = new ThreadListenerQueue();
        DomainPathStorage domainPathStorage = new DomainPathStorage();
        public Flow()
        {
            string path = @"C:\Users\bnaveen\source\repos\AdvancedWebServer\AdvancedWebServer\bin\Debug\netcoreapp2.2\8080/";
            domainPathStorage.AddPath("http://localhost:8080/", path);
            path = @"C:\Users\bnaveen\source\repos\AdvancedWebServer\AdvancedWebServer\bin\Debug\netcoreapp2.2\3030/";
            domainPathStorage.AddPath("http://localhost:3030/", path);
            domainPathStorage.AddPath("http://localhost:9090/", "LeapYear");
        }
        public void Listening()
        {
            Listener listener = new Listener(queue, domainPathStorage);
                listener.Start(); 
        }
        public void Assigning()
        {
            while (true)
            {
                while (queue.GetQueue().Count > 0)
                {
                    Console.WriteLine($"Active Connections:{queue.GetQueue().Count}");
                    HttpListenerContext httpListenerContext = queue.Dequeue();
                    Dipatcher dipatcher = new Dipatcher(httpListenerContext);
                    dipatcher.ParseUrl();
                    if (dipatcher.GetFilename() == "favicon.ico")
                        continue;
                    Response response = new Response(httpListenerContext);
                    var domainDictionary = domainPathStorage.GetDomainsAndPaths();
                    if (httpListenerContext.Request.HttpMethod == "POST")
                    {
                        PostHandler postHandler = new PostHandler(httpListenerContext);
                        if (domainDictionary[dipatcher.GetDomain()] == dipatcher.GetFilename())
                        {
                            ApiFactory apiFactory = new ApiFactory(dipatcher.GetFilename());
                            IApiFunction api = apiFactory.ApiFunctionFactory();
                            
                            var answer = api.Compute(postHandler.jObject);
                            response.SendReponse(postHandler.ConvertJsonObject(answer));
                        }
                       else
                        {
                            httpListenerContext.Response.StatusCode = 404;
                            JObject answer = new JObject();
                            answer["API"] = "Notfound";
                            response.SendReponse(postHandler.ConvertJsonObject(answer));
                        }
                    }
                    else
                    {
                        FileHandler fileHandler = new FileHandler(domainDictionary[dipatcher.GetDomain()], dipatcher.GetFilename());
                        response.SendReponse(fileHandler.ConvertFileDataBytes());
                    }  
                }
            } 
        }
        public void Start()
        {
           
                Thread threadListen = new Thread(() => Listening());
                Thread threadRead = new Thread(() => Assigning());
                threadListen.Start();
                threadRead.Start();  
        }
    }
}
