using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    public class DatabaseContainer : ISystemeDatabaseModule
    {

        private Dictionary<string, IDatabase> databases;
        private ISysteme systeme;

        public DatabaseContainer() 
        {
            this.databases = new Dictionary<string, IDatabase>();
            IDatabase systemDatabase = DefaultDataConstructor.CreateSystemDatabase();
            this.databases.Add(systemDatabase.databaseName, systemDatabase);
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

        public string GetDefaultDatabaseName()
        {
            return null;
        }

        public void ActToAdd(IDatabase database)
        {
            
        }

        public void ActToAdd(IDatabase database, ITable table)
        {
            
        }

        public void ActToRemove(IDatabase database)
        {
            
        }

        public void ActToRemove(IDatabase database, ITable table)
        {
            
        }

        public void TableModified(IDatabase database, ITable table)
        {
            
        }

        public void SetSysteme(ISysteme system)
        {
            this.systeme = system;
        }

        public string GetModuleKey()
        {
            return SystemeConstants.SystemeDatabaseModule;
        }

        public void AcoplateTheModule()
        {
            
        }

        public bool IsAcoplated()
        {
            return true;
        }
    }
}
