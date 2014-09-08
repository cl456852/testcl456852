using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using DAL;
using System.IO;
using MODEL;
using System.Reflection;
using System.Collections;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.sortTest();
            Console.Read();
        }

        void jsTest()
        {
            var engine = new Jurassic.ScriptEngine();
            byte[] b = Encoding.Default.GetBytes("8.35.201.103");
            Console.WriteLine( Convert.ToBase64String(b));
        }

        void sortTest()
        {
            ArrayList list = new ArrayList();
            list.Add("a");
            list.Add("abc");
            list.Add("bac");
            list.Add("b");
            list.Add("4g");
            list.Add("a");
            list.Sort();
            foreach (string a in list)
                Console.WriteLine(a);
        }

    }
}
