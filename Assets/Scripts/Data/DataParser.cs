using System;

namespace TimeTestApp.Data
{
    public static class DataParser
    {
        public static DateTime ParseToDateTime(string data)
        {
            if (DateTime.TryParse(data, out DateTime result))
            {
                var newDateTime = new DateTime();
                newDateTime = newDateTime.AddSeconds(result.Second);
                newDateTime = newDateTime.AddMinutes(result.Minute);
                newDateTime = newDateTime.AddHours(result.Hour);
                return newDateTime;
            }
            else
                throw new Exception("DateTime parse failed");
        }
    }
}

