using MiniSQL.Classes;
using MiniSQL.SystemeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ISystemeDatabaseModule : IActiveSystemModule
    {
        IDatabaseContainer GetDatabaseContainer();
        string GetDefaultDatabaseName();
        IDatabase GetDatabase(string databaseName);
        void AddDatabase(IDatabase database);
    }
}
