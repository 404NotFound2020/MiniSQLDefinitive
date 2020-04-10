using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface IDatabaseContainer
    {

        IDatabase GetDatabase(string databaseName);
        bool ExistDatabase(string databaseName);
        void AddDatabase(IDatabase database);
        void RemoveDatabase(string databaseName);
        int GetNumbersOfDatabases();        
        string GetDefaultDatabaseName();

    }
}
