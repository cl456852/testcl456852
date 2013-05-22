using System;
using System.Reflection;
using System.Collections.Generic;
using Lxt2.Cbip.Api.Code.Cbip20;
using System.Threading;
using System.Text;


namespace Lxt2.Cbip.Api.StaticInfo
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class StaticTool
    {
        public static string ObjToString(object o)
        {
            string log = "";
            Type t = o.GetType();

            log += t.Name + "   ";
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object value1 = pi.GetValue(o, null);
                string name = pi.Name;

                if (name != "BuffHead" && name != "BuffBody")
                {
                    string s = "";// pi.Name + ":" + value1.ToString();
                    if (value1.GetType().Name.IndexOf("List") >= 0)
                    {
                        string str = value1.ToString();
                        string strListType = str.Substring(str.LastIndexOf(".") + 1, str.Length - str.LastIndexOf(".") - 2);
                        if (strListType == "Individual")
                        {
                            List<Individual> iddList = (List<Individual>)value1;

                            foreach (Individual idd in iddList)
                            {
                                s = pi.Name + ":[" + ObjToString(idd) + "]";
                            }
                        }
                        else if (strListType == "ResourceContent")
                        {
                            List<ResourceContent> iddList = (List<ResourceContent>)value1;

                            foreach (ResourceContent idd in iddList)
                            {
                                s = pi.Name + ":[" + ObjToString(idd) + "]";
                            }
                        }
                        //else if (strListType == "")
                        //{
                        //    List<> iddList = (List<Individual>)value1;

                        //    foreach (Individual idd in iddList)
                        //    {
                        //        s = pi.Name + ":[" + ObjToString(idd) + "]";
                        //    }
                        //}

                        //s = pi.Name + ":[" + ObjToString(value1) + "]";
                        //s = pi.Name + ":" + value1.ListToString();

                    }
                    else if (value1.GetType().Name.IndexOf("Byte") >= 0)
                    {
                        s = pi.Name + ":" + value1.ToString();
                    }
                    else
                    {
                        s = pi.Name + ":" + value1.ToString();
                    }


                    log += s + " ";
                }

            }
            //Console.WriteLine( log);
            return log;
        }

        static object o = new object();

        static int squence = 0;
        /// <summary>
        /// 生成SquenceID
        /// </summary>
        /// <returns></returns>
        public static long getSquenceID()
        {



            DateTime d = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0:0000}", StaticHandle.Config.ClientID));
            sb.Append(d.Year.ToString()[3]);
            sb.Append(string.Format("{0:00}", d.Month));
            sb.Append(string.Format("{0:00}", d.Day));
            sb.Append(string.Format("{0:00}", d.Hour));
            sb.Append(string.Format("{0:00}", d.Minute));
            sb.Append(string.Format("{0:00}", d.Second));

            lock (o)
            {
                //sb.Append(string.Format("{0:0000}", Interlocked.Exchange(ref squence, squence == 9999 ? 0 : Interlocked.Increment(ref squence))));
                if (squence == 9999)
                {
                    squence = 0;
                }
                squence++;
                sb.Append(string.Format("{0:0000}", squence));
            }

            return Convert.ToInt64(sb.ToString());


        }
    }
}
