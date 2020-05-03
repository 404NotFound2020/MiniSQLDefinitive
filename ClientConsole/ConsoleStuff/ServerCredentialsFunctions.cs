using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class ServerCredentialsFunctions
    {
        public static IPAddress AskServerIP()
        {
            IPAddress address;
            string ip = null;
            do
            {
                Console.WriteLine("Give a good ip");
                ip = Console.ReadLine();
            }
            while (!IPAddress.TryParse(ip, out address));
            return address;
        }

        public static int AskServerPort()
        {
            int port;
            do
            {
                Console.WriteLine("Give a good port");
                if (!int.TryParse(Console.ReadLine(), out port)) port = -1;
            }
            while (port < 0 || port > 65554);
            return port;
        }        
    }
}
