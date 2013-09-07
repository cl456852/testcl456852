using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using DAL;
using System.IO;
using MODEL;
using System.Reflection;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.jsTest();
            Console.Read();
        }

        void jsTest()
        {
            var engine = new Jurassic.ScriptEngine();
            byte[] b = Encoding.Default.GetBytes("8.35.201.103");
            Console.WriteLine( Convert.ToBase64String(b));
        }

    }
}
