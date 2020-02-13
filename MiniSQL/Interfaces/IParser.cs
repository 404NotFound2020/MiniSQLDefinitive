using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    interface IParser
    {
        Database GetDatabase(string databaseName);
        bool CreateDatabase(string databaseName);
        bool DropDatabase(string databaseName);

    }
}
