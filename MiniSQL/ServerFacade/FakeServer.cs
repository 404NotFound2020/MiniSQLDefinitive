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
            QueryFactory.GetQueryFactory().SetContainer(Systeme.GetSystem());
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
            query.ValidateParameters();
            query.Execute();            
            return query.GetResult();        
        }

        public void SaveShit() 
        {
            Systeme.GetSystem().SaveData();
        }

    }
}
