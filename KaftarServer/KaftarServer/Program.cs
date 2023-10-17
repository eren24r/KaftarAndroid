using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaftarServer
{
    class Program
    {
        const int PORT = 90;
        static void Main(string[] args)
        {
            string host = System.Net.Dns.GetHostName();
            System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            Console.WriteLine("Ip adress " + ip.ToString());

            Server server = new Server(PORT, Console.Out);
            server.Work();
        }
    }
}
