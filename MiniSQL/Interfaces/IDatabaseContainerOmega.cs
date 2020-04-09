using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface IDatabaseContainerOmega
    {

        void AddDatabase(Database database);
        bool ExistDatabase(string databaseName);        
        void RemoveDatabase(string databaseName);
        void AddTable(string databaseName, string tableName);
        bool ExistTable(string databaseName, string tableName);
        void RemoveTable(string databaseName, Table table);

    }
}
