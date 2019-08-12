namespace AdvancedWebServer
{
    public class LeapYear
    {
        public bool IsLeapYear(string _year)
        {
            var year = System.Int32.Parse(_year);
            if (year % 4 == 0 && year % 100 != 0)
                return true;
            else if (year % 400 == 0)
                return true;
            else
                return false;
        }
    }
}
