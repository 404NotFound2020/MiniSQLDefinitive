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
        public List<Tuple<string, Func<string, string>>> protocolesAndTheirConsolePatterns;
        public static string NAINCG = "[^\\* ,<=>]";
        public static string wherePatern = "WHERE " + NAINCG + "+[<=>]" + NAINCG + "+;";
        public static string fromPattern = "FROM (?:("+NAINCG+"+;)|(" + NAINCG + "+ "+ wherePatern+"))";
        public static string selectPattern = "^SELECT (?:(\\*)|(" + NAINCG + "+)(?:,(" + NAINCG + "+))*) " + fromPattern + "$";

        /**
         * SELECT (?:(\*)|([^\* ,<=>]+)(?:,([^\* ,<=>]+))*) FROM (?:([^\* ,<=>]+;)|([^\* ,<=>]+ WHERE [^\* ,<=>]+[<=>][^\* ,<=>]+;))
         */
        private QueryVerifier() 
        {
            this.protocolesAndTheirConsolePatterns = new List<Tuple<string, Func<string, string>>>();        
        }

        private void AddTheProtocolesFunctions() 
        { 
        
        
        }

       


        public static QueryVerifier GetQueryVerifier() 
        {
            if (queryVerifier == null) queryVerifier = new QueryVerifier();
            return queryVerifier;
        }

    }
}
