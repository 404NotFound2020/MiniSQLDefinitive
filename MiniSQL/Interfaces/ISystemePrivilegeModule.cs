using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ISystemePrivilegeModule : IActiveSystemModule
    {
        bool CheckProfileTablePrivileges(string profileName, string databaseName, string tableName, string privilegeType);

    }
}
