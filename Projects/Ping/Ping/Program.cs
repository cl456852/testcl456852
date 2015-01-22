using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
namespace PingTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Ping ping = new Ping();

            byte[] buffer = new byte[250];

            ping.PingCompleted += new PingCompletedEventHandler(ping_PingCompleted);

            for (int i = 0; i < 5; i++)
            {

                ping.SendAsync("www.baidu.com", 1000, buffer);

                System.Threading.Thread.Sleep(1200);//注意睡眠时间 

            }

        }

        static void ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            var reply = e.Reply;
            Console.WriteLine("来自主机:{0},大小:{1},结果:{2},耗时:{3}ms", reply.Address, reply.Buffer.Length, reply.Status, reply.RoundtripTime);
        }
    }
}
