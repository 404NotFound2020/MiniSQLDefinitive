using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class NoCheckPrivilegeModule : ISystemePrivilegeModule
    {
        private static NoCheckPrivilegeModule noCheckPrivilegeModule;

        private NoCheckPrivilegeModule()
        {

        }

        public static NoCheckPrivilegeModule GetNoCheckPrivilegeModule() {
            if (noCheckPrivilegeModule == null) noCheckPrivilegeModule = new NoCheckPrivilegeModule();
            return noCheckPrivilegeModule;
        }

        public void AcoplateTheModule()
        {

        }

        public void ActToAdd(IDatabase database)
        {

        }

        public void ActToAdd(IDatabase database, ITable table)
        {

        }

        public void ActToRemove(IDatabase database)
        {

        }

        public void ActToRemove(IDatabase database, ITable table)
        {

        }

        public bool CheckIsAutorizedToExecuteSecurityQuery(string username)
        {
            return true;
        }

        public bool CheckProfileDatabasePrivileges(string username, string database, string privilegeType)
        {
            return true;
        }

        public bool CheckProfileTablePrivileges(string username, string databaseName, string tableName, string privilegeType)
        {
            return true;
        }

        public string GetModuleKey()
        {
            return null;
        }

        public ISysteme GetSysteme()
        {
            return null;
        }

        public bool IsAcoplated()
        {
            return true;
        }

        public void SetSysteme(ISysteme system)
        {

        }

        public void TableModified(IDatabase database, ITable table)
        {

        }
    }
}