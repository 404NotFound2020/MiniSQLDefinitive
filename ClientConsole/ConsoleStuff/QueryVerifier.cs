using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class QueryVerifier
    {
        private static QueryVerifier queryVerifier;
        private List<Tuple<string, Func<string, string>>> protocolesAndTheirConsolePatterns;

        private QueryVerifier() 
        {
            this.protocolesAndTheirConsolePatterns = new List<Tuple<string, Func<string, string>>>();
        }

        public static QueryVerifier GetQueryVerifier() 
        { 
        
        }
    }
}
