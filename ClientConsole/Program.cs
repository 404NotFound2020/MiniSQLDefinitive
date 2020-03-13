using ClientConsole.ConsoleStuff;
using MiniSQL.Initializer;
using MiniSQL.ServerFacade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Inicializate();
        }

        private static void Inicializate() 
        {
            GetRegex();
            StartConsole();
        }

        private static void GetRegex() 
        {
            string[] stringfiedRegex = FakeServer.GetFakeServer().ReturnRegex();
            QueryVerifier queryVerifier = QueryVerifier.GetQueryVerifier();
            for(int i = 0; i < stringfiedRegex.Length; i++) 
            {
                queryVerifier.AddPattern(stringfiedRegex[i]);
            }        
        }

        private static void StartConsole() 
        {
            string lineOfCocain;
            string message;
            while(!(lineOfCocain = Console.ReadLine()).Equals("exit")) 
            {
                message = "Go to fuck yourself stupid shitty idiot";
                if (QueryVerifier.GetQueryVerifier().EvaluateQuery(lineOfCocain)) message = Requester.GetRequester().SendRequest(TransactionCreator.GetTransactionCreator().CreateGroupDependingXML(QueryVerifier.GetQueryVerifier().queryMatch));
                Console.WriteLine(message);
            }

        }

    }
}
