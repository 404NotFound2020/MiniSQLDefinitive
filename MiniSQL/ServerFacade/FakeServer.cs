using MiniSQL.Constants;
using MiniSQL.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.ServerFacade
{
    public class FakeServer
    {
        private static FakeServer fakeServer;

        private FakeServer() 
        { 
        
        }

        public static FakeServer GetFakeServer() 
        {
            if (fakeServer == null) fakeServer = new FakeServer();
            return fakeServer;
        }

        public string[] ReturnRegex() 
        { 
            return new string[]{RequestAndRegexConstants.selectPattern, RequestAndRegexConstants.insertPattern, RequestAndRegexConstants.updatePattern, RequestAndRegexConstants.createPattern, RequestAndRegexConstants.deletePattern, RequestAndRegexConstants.dropPattern};
        }

        public string ReceiveRequest(string request) {
            QueryFactory.GetQueryFactory().GetQuery(new Request(request));
            return null;        
        }


    }
}
