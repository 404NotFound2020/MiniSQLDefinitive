using MiniSQL.ServerFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minisql_tester.SomeTestStuff
{
    public class DirectRequester
    {
        private static DirectRequester requester;

        private DirectRequester()
        {

        }

        public string SendRequest(string query)
        {
            return FakeServer.GetFakeServer().ReceiveRequest(query);
        }

        public static DirectRequester GetRequester()
        {
            if (requester == null) requester = new DirectRequester();
            return requester;
        }

    }
}
