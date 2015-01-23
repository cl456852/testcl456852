using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.IO;
namespace PingTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Ping ping = new Ping();

            byte[] buffer = new byte[250];
            HashSet<string> set = readFile();
            ping.PingCompleted += new PingCompletedEventHandler(ping_PingCompleted);
            foreach(string name in set)
            {

                ping.SendAsync(name, 1000, buffer);

                System.Threading.Thread.Sleep(1200);//注意睡眠时间 

            
            }
        }

        static void ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            var reply = e.Reply;
            Console.WriteLine("来自主机:{0},大小:{1},结果:{2},耗时:{3}ms", reply.Address, reply.Buffer.Length, reply.Status, reply.RoundtripTime);
        }

        static HashSet<string> readFile()
        {
            string res="";
            HashSet<string> set = new HashSet<string>();
            using (StreamReader sr = new StreamReader("a.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    if (!res.Contains(sr.ReadLine().Split('\t')[0]))
                        res += sr.ReadLine().Split('\t')[0] + "\r\n";
                }
            }
            WriteFile("b.txt", res);
            return set;
        }

        public static void WriteFile(string path, string content)
        {
            FileStream fs1 = new FileStream(path, FileMode.Create);
            //实例化一个StreamWriter-->与fs相关联  
            StreamWriter sw1 = new StreamWriter(fs1);
            //开始写入  
            sw1.Write(content);
            //清空缓冲区  
            sw1.Flush();
            //关闭流  
            sw1.Close();
            fs1.Close();
        }
    }
}
