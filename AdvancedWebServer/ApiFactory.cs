using System;
namespace AdvancedWebServer
{
    public class ApiFactory
    {
        string name;
        public ApiFactory(string name)
        {
            this.name = name;
        }
        public IApiFunction ApiFunctionFactory()
        {
            switch(name)
            {
                case "LeapYear":
                    return new LeapYear();
                default:
                    throw NotImplementedException();
            }
        }

        private Exception NotImplementedException()
        {
            Console.WriteLine("ApiNotFound");
            throw new NotImplementedException();
        }
    }
}
