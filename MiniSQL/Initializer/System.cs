using MiniSQL.Classes;
using MiniSQL.ConfigurationClasses;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public class System : IDatabaseContainer
    {

        private Dictionary<string, Database> activeDatabases;        
        private SystemConfiguration configuration;
        private static System system = new System();

        private System() 
        {
            
        }

        public static System GetSystem() 
        {
            return system;
        }

        public Database GetDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public bool ExistDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void AddDatabase(Database database)
        {
            throw new NotImplementedException();
        }

        public void RemoveDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }
    }
}
