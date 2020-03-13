using MiniSQL.Constants;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using System;
using System.Collections.Generic;
using System.IO;
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
            AbstractQuery query = QueryFactory.GetQueryFactory().GetQuery(new Request(request));
            if(query.ValidateParameters()) query.Execute(); //The if can be omited, because the same query controls if validateParameter return true            
            return query.GetResult();        
        }

        public void SaveShit() 
        {
            Systeme.GetSystem().SaveData();
        }

    }
}
