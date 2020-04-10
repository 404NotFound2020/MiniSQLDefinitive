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

        private Dictionary<string, IDatabase> databases;

        public DatabaseContainer() 
        {
            this.databases = new Dictionary<string, IDatabase>();
        }

        public void AddDatabase(IDatabase database)
        {
            this.databases.Add(database.databaseName, database);
        }

        public bool ExistDatabase(string databaseName)
        {
            return this.databases.ContainsKey(databaseName);
        }

        public IDatabase GetDatabase(string databaseName)
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

        public void SaveData(IDatabase database)
        {
            
        }

        public void SaveTable(IDatabase database, ITable table)
        {
            
        }

        public void RemoveTable(IDatabase database, ITable table)
        {
            
        }

        public string GetDefaultDatabaseName()
        {
            return null;
        }
    }
}
