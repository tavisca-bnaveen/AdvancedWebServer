using Newtonsoft.Json.Linq;
namespace AdvancedWebServer
{
    public interface IApiFunction
    {
         JObject Compute(JObject jObject);
    }
}
