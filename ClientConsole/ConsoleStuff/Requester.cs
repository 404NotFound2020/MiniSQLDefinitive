using MiniSQL.ServerFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class Requester
    {
        private static Requester requester;

        private Requester() 
        { 
        
        }

        public string SendRequest(string query) {
            return FakeServer.GetFakeServer().ReceiveRequest(query);                    
        }

        public static Requester GetRequester() 
        {
            if (requester == null) requester = new Requester();
            return requester;
        }

    }
}
