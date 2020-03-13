using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class QueryVerifier
    {
        private static QueryVerifier queryVerifier;
        private List<Regex> querysPatterns;
        public Match queryMatch;

        private QueryVerifier() 
        {
            this.querysPatterns = new List<Regex>();
        }

        public void AddPattern(string stringPattern) 
        {
            this.querysPatterns.Add(new Regex(stringPattern));
        }

        public bool EvaluateQuery(string stringfiedQuery) {
            bool b = false;
            Match match = null;
            IEnumerator<Regex> enumerator = this.querysPatterns.GetEnumerator();            
            while (enumerator.MoveNext() && !b) 
            {
                match = enumerator.Current.Match(stringfiedQuery);
                b = match.Success;
            }
            if (b) this.queryMatch = match;
            Console.WriteLine(TransactionCreator.CreateGroupDependingXML(match));
            Console.ReadLine();
            return b;
        }

        public static QueryVerifier GetQueryVerifier() 
        {
            if (queryVerifier == null) queryVerifier = new QueryVerifier();
            return queryVerifier;
        }

    }
}
