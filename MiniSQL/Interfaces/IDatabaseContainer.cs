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
        void SaveData();
        void SaveData(IDatabase database);
        void SaveTable(IDatabase database, ITable table);
        void RemoveTable(IDatabase database, ITable table);
        string GetDefaultDatabaseName();

    }
}
