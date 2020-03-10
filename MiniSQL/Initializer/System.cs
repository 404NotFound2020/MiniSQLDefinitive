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
            return activeDatabases[databaseName];
        }

        public bool ExistDatabase(string databaseName)
        {
            if (activeDatabases.ContainsKey(databaseName))
                return true;
            else
                return false;
        }

        public void AddDatabase(Database database)
        {
            activeDatabases.Add(database.databaseName,database);
        }

        public void RemoveDatabase(string databaseName)
        {
            activeDatabases.Remove(databaseName);
        }

        public int GetNumbersOfDatabases()
        {
            return this.activeDatabases.Count;
        }
    }
}
