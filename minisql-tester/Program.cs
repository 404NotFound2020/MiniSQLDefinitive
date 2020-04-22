using ClientConsole.ConsoleStuff;
using MiniSQL.ServerFacade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minisql_tester
{
    class Program
    {
        static void Main(string[] args)
        {
            GetRegex();
            string inputfile = "input-file.txt";
            string outputfile = "outfile-results.txt";
            int i = 0;
            while(i < args.Length) 
            { 
                if(args[i] == "-i") {
                    i = i + 1;
                    inputfile = args[i];
                }
                else if(args[i] == "-o")
                {
                    i = i + 1;
                    outputfile = args[i];
                }
                i = i + 1;
            }
            DoTheTest(inputfile, outputfile);
        }

        public static void DoTheTest(string filePath, string outFilePath)
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
                    else
                    {
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
            FakeServer.GetFakeServer().SaveShit();
        }
        
        private static double GetDiffAndAct(DateTime date, DateTime date2)
        {
            TimeSpan span = date2 - date;
            return span.TotalMilliseconds / 1000;
        }
        
        private static void GetRegex()
        {
            string[] stringfiedRegex = FakeServer.GetFakeServer().ReturnRegex();
            QueryVerifier queryVerifier = QueryVerifier.GetQueryVerifier();
            for (int i = 0; i < stringfiedRegex.Length; i++)
            {
                queryVerifier.AddPattern(stringfiedRegex[i]);
            }
        }

    }

}

