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
        const string path = @"C:\Users\Administrator\AppData\Roaming\uTorrent\uTorrent.exe";
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
                if (p.ProcessName == "uTorrent")
                {
                    process = p;
                    break;
                }
            }
            if(process==null)
                process = Process.Start(path);
        }

        void checkResponse()
        {
            while (true)
            {
                if (!process.Responding)
                    break;
                Thread.Sleep(10*60*1000);
            }
            Console.WriteLine(DateTime.Now + "   NO RESPONSE RESTART");
            try
            {
                process.Kill();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Process myProcess = Process.Start(path);
        }
    }
}
