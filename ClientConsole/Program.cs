using ClientConsole.ConsoleStuff;
using NetworkUtilities.Requests;
using NetworkUtilities.Transactions;
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
            ConnectToServer(ServerCredentialsFunctions.AskServerIP().ToString(), ServerCredentialsFunctions.AskServerPort());
            Console.ReadLine();
        }
        
        private static void ConnectToServer(string ip, int port)
        {
            tcpClient = new TcpClient(ip, port);
            NetworkStream stream = tcpClient.GetStream();
            string regexResponse = SendAndReceive.ReceiveMessage(stream, 256);
            SetRegex(regexResponse);
            StartConsole(stream);
        }

        private static void StartConsole(NetworkStream stream)
        {
            string lineOfCocain;
            string message;
            while (!(lineOfCocain = Console.ReadLine()).Equals("EXIT;"))
            {
                message = "Go to fuck yourself stupid shitty idiot";
                if (QueryVerifier.GetQueryVerifier().EvaluateQuery(lineOfCocain))
                {
                    SendAndReceive.SendMessage(stream, TransactionCreator.GetTransactionCreator().CreateGroupDependingXML(QueryVerifier.GetQueryVerifier().queryMatch));
                    message = (new XmlMessage(SendAndReceive.ReceiveMessage(stream, 256))).GetElementsContentByTagName("payload")[0];
                    
                }
                Console.WriteLine(message);
            }
            QueryVerifier.GetQueryVerifier().EvaluateQuery(lineOfCocain);
            SendAndReceive.SendMessage(stream, TransactionCreator.GetTransactionCreator().CreateGroupDependingXML(QueryVerifier.GetQueryVerifier().queryMatch));
            message = (new XmlMessage(SendAndReceive.ReceiveMessage(stream, 256))).GetElementsContentByTagName("payload")[0];
            Console.WriteLine(message);
            tcpClient.Close();
        }

        private static void SetRegex(string message)
        {
            XmlMessage request = new XmlMessage(message);
            QueryVerifier queryVerifier = QueryVerifier.GetQueryVerifier();
            string[] regex = request.GetElementsContentByTagName("regex");
            for (int i = 0; i < regex.Length; i++) queryVerifier.AddPattern(regex[i]);
        }

    }
}
