using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface IUbicationManager
    {
        void CreateDatabaseDirectory(string databaseName);
        void CreateTableFiles(string tableName);
        void DeleteDatabaseDirectory(string databaseName);
        void DeleteTableFiles(string tableName);

    }
}
