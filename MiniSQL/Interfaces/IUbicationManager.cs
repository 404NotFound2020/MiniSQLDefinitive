using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface IUbicationManager
    {
        string GetDatabaseFilePath(string databaseName);
        string GetTableStructureFilePath(string tableName);
        string GetTableDataFilePath(string tableName);

    }
}
