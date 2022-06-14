using UnityEngine;
using System.Threading;

namespace TimeTestApp.Data
{
    public static class DataLoader
    {
        public static string GetTimeFromWeb(string link)
        {
            var www = new WWW(link);
            while (!www.isDone && www.error == null)
                Thread.Sleep(1);

            var dataString = www.responseHeaders["Date"];
            return dataString;
        }
    }
}

