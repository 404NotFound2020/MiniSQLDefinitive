using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class AbstractParser
    {
        private IUbicationManager ubicationManager;

        public abstract Database LoadDatabase(string databaseName);
        public abstract Tuple<Table, List<Tuple<string, string, string>>> LoadTable(string databaseName, string tableName);
        public abstract void SaveTable(Database database, Table table);
        public abstract void SaveDatabase(Database database);
        public abstract void DeleteDatabase(string databaseName);
        public abstract void DeleteTable(string databaseName, string tableName);
        public abstract bool ExistDatabase(string databaseName);
        public abstract bool ExistTable(string databaseName, string tableName);

        public abstract string[] GetDatabasesNames();

        public void SetUbicationManager(IUbicationManager ubicationManager) 
        {
            this.ubicationManager = ubicationManager;        
        }

        protected IUbicationManager GetUbicationManager() 
        {
            return this.ubicationManager;
        }

    }
}
