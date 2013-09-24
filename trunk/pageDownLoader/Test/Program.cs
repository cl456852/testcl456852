using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.test();
            Console.ReadLine();
        }

        void test()
        {
            ProcessStartInfo start = new ProcessStartInfo("rasdial");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
            //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
            start.Arguments = "宽带连接 /disconnect";//设置命令参数
            start.CreateNoWindow = true;//不显示dos命令行窗口
            start.RedirectStandardOutput = true;//
            start.RedirectStandardInput = true;//
            start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
            Process p = Process.Start(start);
            Console.WriteLine("disconnected");
            Thread.Sleep(10000);
            ProcessStartInfo start1 = new ProcessStartInfo("rasdial");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
            //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
            start1.Arguments = "宽带连接 300000162885 68819054";//设置命令参数
            start1.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
            Process p1 = Process.Start(start1);
            Console.WriteLine("connected");
        }
    }
}
