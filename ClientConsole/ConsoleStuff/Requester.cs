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
            string message = null;
            QueryVerifier queryVerifier = QueryVerifier.GetQueryVerifier();
            if (queryVerifier.EvaluateQuery(query)) 
            {
                //message = this.SendXMLRequest(queryVerifier.actualMatchAndAsociatedFunction.Item2.Invoke(queryVerifier.actualMatchAndAsociatedFunction.Item1));
            }
            return message;        
        }

        public string SendXMLRequest(string xmlRequest) {
            return null;
        }


        public static Requester GetRequester() 
        {
            if (requester == null) requester = new Requester();
            return requester;
        }



    }
}
