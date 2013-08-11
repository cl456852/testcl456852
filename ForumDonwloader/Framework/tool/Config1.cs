using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Framework.tool
{
    public class Config1
    {
        static object o=new object();
        private static string lastVisit = "1374857595";

        public static void setLastVisit(string s)
        {
            lock (o)
            {
                lastVisit = s;
            }
        }
        public static string getLastVisit()
        {
            return lastVisit;
        }

        static Dictionary<string, Cookie> dic;

        private static CookieContainer cookieContainer ;

        public static CookieContainer getCookies()
        {
            lock (o)
            {
                if (dic == null)
                {
                    Cookie LastVisit = new Cookie("LastVisit", Config1.getLastVisit(), "/", "rarbg.com");
                    Cookie __utma = new Cookie("__utma", "211336342.1152591947.1370187873.1374304584.1374853845.39", "/", ".rarbg.com");
                    Cookie __utmb = new Cookie("__utmb", "211336342.35.10.1374853845", "/", ".rarbg.com");
                    Cookie __utmc = new Cookie("__utmc", "211336342", "/", ".rarbg.com");
                    Cookie __utmz = new Cookie("__utmz", "211336342.1370187873.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", ".rarbg.com");
                    Cookie bSbTZF2j = new Cookie("bSbTZF2j", "6BdPQ9qs", "/", "rarbg.com");
                    dic = new Dictionary<string, Cookie>();
                    dic.Add("LastVisit", LastVisit);
                    dic.Add("__utma", __utma);
                    dic.Add("__utmb", __utmb);
                    dic.Add("__utmc", __utmc);
                    dic.Add("__utmz", __utmz);
                    dic.Add("bSbTZF2j", bSbTZF2j);
  
                }
                cookieContainer = new CookieContainer();
                foreach (string name in dic.Keys)
                {
                    cookieContainer.Add(dic[name]);
                }
                return cookieContainer;
            }
        }

        public static void setCookies(CookieCollection cookies)
        {
            foreach (Cookie c in cookies)
            {
                dic.Remove(c.Name);
                dic.Add(c.Name, c);
            }
            foreach (Cookie c in cookieContainer.GetCookies(new Uri("http://rarbg.com/")))
            {
                Console.WriteLine(c.Name+"  "+ c.Value);
            }
        }

    }
}
