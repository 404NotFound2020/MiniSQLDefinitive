using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MiniSQL.Classes;

namespace MiniSQL.ConcurrenceClasses
{
    public class ClientThread
    {
        private StreamReader reader;
        private StreamWriter writer;
        private Database targetDatabase;

        public ClientThread(Stream clientStream) 
        {
            this.reader = new StreamReader(clientStream);
            this.writer = new StreamWriter(clientStream);
        }





    }
}
