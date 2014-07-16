using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace MailConsole
{
    class ProcessMoitor
    {
        Process process;
        public void start()
        {

            while (true)
            {
                findProcess();
                checkResponse();
            }
        }

        void findProcess()
        {
            Process[] processes;
            //Get the list of current active processes.
            processes = System.Diagnostics.Process.GetProcesses();
            //Grab some basic information for each process.
            foreach (Process p in processes)
            {
                if (p.ProcessName == "UI1")
                {
                    process = p;
                    break;
                }
            }
            if(process==null)
                process = Process.Start(@"D:\svn\复件 Demo\UI1\bin\Debug\UI1.exe");
        }

        void checkResponse()
        {
            while (true)
            {
                if (!process.Responding)
                    break;
                Thread.Sleep(60000);
            }
            Console.WriteLine(DateTime.Now + "   NO RESPONSE RESTART");
            process.Kill();
            Process myProcess = Process.Start(@"D:\svn\复件 Demo\UI1\bin\Debug\UI1.exe");
        }
    }
}
