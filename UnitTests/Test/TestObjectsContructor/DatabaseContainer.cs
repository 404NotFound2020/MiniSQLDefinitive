using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    public class DatabaseContainer : IDatabaseContainer
    {

        private Dictionary<string, Database> databases;

        public DatabaseContainer() 
        {
            this.databases = new Dictionary<string, Database>();
        }

        public void AddDatabase(Database database)
        {
            this.databases.Add(database.databaseName, database);
        }

        public bool ExistDatabase(string databaseName)
        {
            return this.databases.ContainsKey(databaseName);
        }

        public Database GetDatabase(string databaseName)
        {
            return this.databases[databaseName];
        }

        public void RemoveDatabase(string databaseName)
        {
            this.databases.Remove(databaseName);
        }
        public int GetNumbersOfDatabases()
        {
            return this.databases.Count;
        }

        public void SaveData()
        {
            
        }

        public void SaveData(Database database)
        {
            
        }

        public void SaveTable(Database database, Table table)
        {
            
        }

        public void RemoveTable(Database database, Table table)
        {
            
        }

        public string GetDefaultDatabaseName()
        {
            return null;
        }
    }
}
