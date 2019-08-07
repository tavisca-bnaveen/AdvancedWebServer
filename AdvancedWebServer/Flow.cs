using System;
using System.Net;
using System.Threading;

namespace AdvancedWebServer
{
    public class Flow
    {
        ThreadListenerQueue queue = new ThreadListenerQueue();
        DomainPathStorage domainPathStorage = new DomainPathStorage();
        public Flow()
        {
            string dummy = @"C:\Users\bnaveen\source\repos\AdvancedWebServer\AdvancedWebServer\bin\Debug\netcoreapp2.2\8080/";
            domainPathStorage.AddPath("http://localhost:8080/", dummy);
            dummy = @"C:\Users\bnaveen\source\repos\AdvancedWebServer\AdvancedWebServer\bin\Debug\netcoreapp2.2\3030/";
            domainPathStorage.AddPath("http://localhost:3030/", dummy);
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
                    var domainDictionary = domainPathStorage.GetDomainsAndPaths();
                    FileHandler fileHandler = new FileHandler(domainDictionary[dipatcher.GetDomain()], dipatcher.GetFilename());
                    Response response = new Response(httpListenerContext);
                    response.SendReponse(fileHandler.ConvertFileDataBytes());
                    

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
