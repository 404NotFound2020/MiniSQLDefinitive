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
        public abstract Table LoadTable(string tableName);
        public abstract bool SaveTable(Database database, Table table);
        public abstract bool SaveDatabase(Database database);
        public abstract bool DeleteDatabase(string databaseName);
        public abstract bool DeleteTable(string databaseName, string tableName);
        public abstract bool ExistDatabase(string databaseName);
        public abstract bool ExistTable(string databaseName, string tableName);


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
