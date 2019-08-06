using System.Collections.Generic;
namespace AdvancedWebServer
{
    public class DomainPathStorage
    {
        Dictionary<string, string> domainPath = new Dictionary<string, string>();
        public void AddPath(string domain,string path)
        {
            domainPath[domain] = path;
        }
        public string GetPathByDomain(string domain)
        {
            return domainPath[domain];
        }
        public Dictionary<string, string> GetDomainsAndPaths()
        {
            return domainPath;
        }
    }
}
