using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ISystemePrivilegeModule : IActiveSystemModule
    {
        bool CheckProfileTablePrivileges(string username, string databaseName, string tableName, string privilegeType);
        bool CheckProfileDatabasePrivileges(string username, string database, string privilegeType);
        bool CheckIsAutorizedToExecuteSecurityQuery(string username);
    }
}
