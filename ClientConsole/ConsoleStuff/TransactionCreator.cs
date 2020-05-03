using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class TransactionCreator
    {
        private static TransactionCreator transactionCreator;

        private TransactionCreator() 
        { 
        
        }

        public static TransactionCreator GetTransactionCreator() 
        {
            if (transactionCreator == null) transactionCreator = new TransactionCreator();
            return transactionCreator;
        }

        public string CreateGroupDependingXML(Match match) 
        {
            string xmlString = "<payload>";
            xmlString = xmlString + "\n <fullQuery><![CDATA[" + match.Groups[0].Value + "]]></fullQuery>";
            for (int i = 1; i < match.Groups.Count; i++) 
            {
                for (int j = 0; j < match.Groups[i].Captures.Count; j++)
                {
                    xmlString = xmlString + "\n <" + match.Groups[i].Name + "><![CDATA[" + match.Groups[i].Captures[j].Value + "]]></" + match.Groups[i].Name + ">";
                }                
            }
            xmlString = xmlString + "\n</payload>";
            return xmlString;        
        }
    }
}
