using ClientConsole.ConsoleStuff;
using MiniSQL.Initializer;
using MiniSQL.ServerFacade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        private static TcpClient tcpClient;

        static void Main(string[] args)
        {
            ConnectToServer(AskServerIP().ToString(), AskServerPort());
            Console.ReadLine();
        }
        
        private static void ConnectToServer(string ip, int port)
        {
            tcpClient = new TcpClient(ip, port);
            NetworkStream stream = tcpClient.GetStream();
            string regexResponse = ReceiveMessage(stream, 256);
            SetRegex(regexResponse);
            StartConsole(stream);
        }

        private static IPAddress AskServerIP()
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

        private static int AskServerPort()
        {
            int port;          
            do {
                Console.WriteLine("Give a good port");
                if (!int.TryParse(Console.ReadLine(), out port)) port = -1;               
            }
            while (port < 0 || port > 65554);
            return port;
        }

        private static string ReceiveMessage(NetworkStream stream, int bufferSize)
        {
            Byte[] data = new Byte[bufferSize];
            Int32 bytes;
            string responseData = "";
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                responseData = responseData + System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            }
            while (stream.DataAvailable);
            return responseData;
        }

        private static void SetRegex(string message)
        {
            Request request = new Request(message);
            QueryVerifier queryVerifier = QueryVerifier.GetQueryVerifier();
            string[] regex = request.GetElementsContentByTagName("regex");
            for (int i = 0; i < regex.Length; i++)
            {
                queryVerifier.AddPattern(regex[i]);
            }
        }

        private static void StartConsole(NetworkStream stream)
        {
            string lineOfCocain;
            string message;
            byte[] msg;
            while (!(lineOfCocain = Console.ReadLine()).Equals("EXIT;"))
            {
                message = "Go to fuck yourself stupid shitty idiot";
                if (QueryVerifier.GetQueryVerifier().EvaluateQuery(lineOfCocain))
                {
                    msg = System.Text.Encoding.ASCII.GetBytes(TransactionCreator.GetTransactionCreator().CreateGroupDependingXML(QueryVerifier.GetQueryVerifier().queryMatch));
                    stream.Write(msg, 0, msg.Length);
                    message = (new Request(ReceiveMessage(stream, 256))).GetElementsContentByTagName("message")[0];
                }
                Console.WriteLine(message);
            }
            QueryVerifier.GetQueryVerifier().EvaluateQuery(lineOfCocain);
            msg = System.Text.Encoding.ASCII.GetBytes(TransactionCreator.GetTransactionCreator().CreateGroupDependingXML(QueryVerifier.GetQueryVerifier().queryMatch));
            stream.Write(msg, 0, msg.Length);
            message = (new Request(ReceiveMessage(stream, 256))).GetElementsContentByTagName("message")[0];
            Console.WriteLine(message);
            tcpClient.Close();
        }



    }
}
