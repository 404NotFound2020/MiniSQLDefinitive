using MiniSQL.Initializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            //standardOutput.WriteLine("eeee");
            
            ConfigurationParser.GetConfigurationParser();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("a", "b");
            dic.Add("a", "c");
            standardOutput.WriteLine(dic.Count());
            
            Console.ReadLine();
        }
    }
}
