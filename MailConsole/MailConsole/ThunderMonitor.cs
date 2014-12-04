using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace MailConsole
{
    class ThunderMonitor
    {
        const string path = @"C:\Program Files (x86)\Thunder Network\Thunder\Program\Thunder.exe";
        
        public void start()
        {

            while (true)
            {
                findProcess();
                Thread.Sleep(30 * 1000);
                
            }
        }

        void findProcess()
        {
            Process process=null;
            Process[] processes;
            //Get the list of current active processes.
            processes = System.Diagnostics.Process.GetProcesses();
            //Grab some basic information for each process.
            foreach (Process p in processes)
            {
                if (p.ProcessName == "Thunder")
                {
                    process = p;
                    break;
                }
            }
            if (process == null)
                process = Process.Start(path);
        }
    }
}
