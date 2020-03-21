using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class Tests
    {
        public static void DoTheTest(string filePath, string outFilePath)
        {
            StreamReader reader = new StreamReader(filePath);
            int numberOfTest = 1;
            string textLine;
            string result = "# TEST" + numberOfTest + "\n";
            DateTime date = DateTime.Now;
            DateTime date2;
            TimeSpan span;

            while ((textLine = reader.ReadLine()) != null)
            {
                if (!textLine.Equals(""))
                {
                    if (QueryVerifier.GetQueryVerifier().EvaluateQuery(textLine)) result = result + Requester.GetRequester().SendRequest(TransactionCreator.GetTransactionCreator().CreateGroupDependingXML(QueryVerifier.GetQueryVerifier().queryMatch)) + "\n";
                    else result = result + "bad sintax";                        
                }
                else
                {
                    date2 = DateTime.Now;
                    span = date2 - date;
                    numberOfTest = numberOfTest + 1;
                    result = result + "TOTAL TIME:" + span.TotalSeconds + "\n\n" + "# TEST" + numberOfTest + "\n";
                    date = date2;
                }
            }
            date2 = DateTime.Now;
            span = date2 - date;
            result = result + "TOTAL TIME:" + span.TotalSeconds;
            Console.WriteLine(result);
        }
    }
}

