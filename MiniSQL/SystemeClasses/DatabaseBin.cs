using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SystemeClasses
{
    public class DatabaseBin : IDatabaseContainer
    {
        public Dictionary<string, IDatabase> activeDatabases;
        private ISystemeDatabaseModule systemeDatabaseModule;

        public DatabaseBin(ISystemeDatabaseModule systemeDatabaseModule)
        {
            this.systemeDatabaseModule = systemeDatabaseModule;
            this.activeDatabases = new Dictionary<string, IDatabase>();
        }

        public void AddDatabase(IDatabase database)
        {
            activeDatabases.Add(database.databaseName, database);
            IEnumerator<IActiveSystemModule> activesModulesEnumerator = this.systemeDatabaseModule.GetSysteme().GetActiveModuleList().GetEnumerator();
            while (activesModulesEnumerator.MoveNext()) activesModulesEnumerator.Current.ActToAdd(database);
        }

        public bool ExistDatabase(string databaseName)
        {
            return activeDatabases.ContainsKey(databaseName);
        }

        public IDatabase GetDatabase(string databaseName)
        {
            return new DatabaseProxy(this.activeDatabases[databaseName], this.systemeDatabaseModule.GetSysteme().GetActiveModuleList());
        }

        public void RemoveDatabase(string databaseName)
        {
            IDatabase database = this.activeDatabases[databaseName];
            IEnumerator<IActiveSystemModule> activesModulesEnumerator = this.systemeDatabaseModule.GetSysteme().GetActiveModuleList().GetEnumerator();
            while (activesModulesEnumerator.MoveNext()) activesModulesEnumerator.Current.ActToRemove(database);
            this.activeDatabases.Remove(databaseName);
        }
    }
}
