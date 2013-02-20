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
            //p.checkCheck();
            MyFileInfo tc = new MyFileInfo();
            tc.Directory = "dddddddddd";
//            Type t = tc.GetType();
//           foreach (PropertyInfo pi in t.GetProperties())
//{
//    object value1 = pi.GetValue(tc, null);//用pi.GetValue获得值
//    string name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
//    //获得属性的类型,进行判断然后进行以后的操作,例如判断获得的属性是整数
//    Console.WriteLine(name);
//}
           Console.WriteLine( tc.GetType().GetProperty("Directory").GetValue(tc,null).ToString());
           Console.WriteLine(tc.GetType().GetProperty("Directory").GetValue(tc,null).GetType());
           Console.WriteLine(typeof(string));
           Console.WriteLine(tc.GetType().GetProperty("Directory").GetValue(tc,null).GetType()==typeof(string));
           Console.Read();
        }

        public void check()
        {
            string a = "fawef.awef";
            string name = a.Substring(0, a.LastIndexOf("."));
            Console.WriteLine(name);
            Console.ReadLine();
        }

        //public void Validate()
        //{
        //    FileDAL fd = new FileDAL();
        //    String[] path = Directory.GetFiles("D:\\temp", "*", SearchOption.TopDirectoryOnly);
        //    MyFileInfo fi = new MyFileInfo(path[0]);
        //    Console.WriteLine(fi.Directory.Root.Name);
        //    Console.WriteLine(fi.Directory.Name);
        //    Console.WriteLine(FileDAL.Validate(fi));
        //    Console.ReadLine();
        //}

        //public void checkCheck()
        //{
        //    MyFileInfo fi = new MyFileInfo("SBVD002.avi");
            
        //    Console.WriteLine( FileDAL.Check(fi));

        //    Console.Read();
        //}

    }
}
