using Newtonsoft.Json.Linq;

namespace AdvancedWebServer
{
    public class LeapYear:IApiFunction
    {
        public JObject Compute(JObject jObjectinput)
        {

            var year = System.Int32.Parse(jObjectinput["year"].ToString());
            JObject jObject = new JObject();
            if (year % 4 == 0 && year % 100 != 0)
                jObject["IsLeapYear"]="true";
            else if (year % 400 == 0)
                jObject["IsLeapYear"] = "true";
            else
                jObject["IsLeapYear"] = "False";
            return jObject;
        }   
    }
}
