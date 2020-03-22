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
        public static void DoTheTest(string filePath, string outFilePath = "outfile-results.txt")
        {
            StreamReader reader = new StreamReader(filePath);
            int numberOfTest = 1;
            string textLine;
            string result = "# TEST" + numberOfTest + "\n";
            double seconds = 0;
            double diff;
            DateTime date;
            while ((textLine = reader.ReadLine()) != null)
            {
                if (!textLine.Equals(""))
                {
                    if (QueryVerifier.GetQueryVerifier().EvaluateQuery(textLine))
                    {
                        date = DateTime.Now;
                        result = result + Requester.GetRequester().SendRequest(TransactionCreator.GetTransactionCreator().CreateGroupDependingXML(QueryVerifier.GetQueryVerifier().queryMatch));
                        diff = GetDiffAndAct(date, DateTime.Now);
                        result = result + " (" + diff + "s)\n";
                        seconds = seconds + diff;
                    }
                    else {
                        result = result + "bad sintax\n";
                    }                        
                }
                else
                {
                    numberOfTest = numberOfTest + 1;
                    result = result + "TOTAL TIME:" + seconds + "s\n\n" + "# TEST " + numberOfTest + "\n";
                    seconds = 0;
                }
            }
            result = result + "TOTAL TIME:" + seconds + "s";
            File.WriteAllText(outFilePath, result);
            Console.WriteLine(result);
        }

        private static double GetDiffAndAct(DateTime date, DateTime date2) 
        {
            TimeSpan span = date2 - date;
            return span.TotalMilliseconds/1000;
        }

    }
}

