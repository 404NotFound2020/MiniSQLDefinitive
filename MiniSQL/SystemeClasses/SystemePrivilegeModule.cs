using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SystemeClasses
{
    public class SystemePrivilegeModule : ISystemePrivilegeModule
    {
        private ISysteme systeme;
        private static SystemePrivilegeModule privilegeModule;

        private SystemePrivilegeModule()
        {

        }

        public static SystemePrivilegeModule GetSystemePrivilegeModule()
        {
            if (privilegeModule == null) privilegeModule = new SystemePrivilegeModule();
            return privilegeModule;
        }

        public void SetSysteme(ISysteme system)
        {
            this.systeme = system;
        }

        public void ActToAdd(IDatabase database)
        {
            ISystemeDatabaseModule databaseModule = (ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule);
            IDatabase systemDatabase = databaseModule.GetDatabaseContainer().GetDatabase(SystemeConstants.SystemDatabaseName);
            ITable profilesPrivilegesOnDatabasesTable = systemDatabase.GetTable(SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            ITable databasesPrivilegesTable = systemDatabase.GetTable(SystemeConstants.DatabasesPrivilegesTableName);
            ITable adminProfileTable = systemDatabase.GetTable(SystemeConstants.NoRemovableProfilesTableName);
            IEnumerator<Row> adminProfilesrowEnumerator = adminProfileTable.GetRowEnumerator();
            IEnumerator<Row> databasesPrivilegesRowEnumerator = databasesPrivilegesTable.GetRowEnumerator();
            Row row;
            while (adminProfilesrowEnumerator.MoveNext())
            {
                while (databasesPrivilegesRowEnumerator.MoveNext())
                {
                    row = profilesPrivilegesOnDatabasesTable.CreateRowDefinition();
                    row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName).data = database.databaseName;
                    row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName).data = adminProfilesrowEnumerator.Current.GetCell(SystemeConstants.ProfileNameColumn).data;
                    row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName).data = databasesPrivilegesRowEnumerator.Current.GetCell(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName).data;
                    profilesPrivilegesOnDatabasesTable.AddRow(row);
                }
                databasesPrivilegesRowEnumerator.Reset();
            }
        }

        public void ActToAdd(IDatabase database, ITable table)
        {
            ISystemeDatabaseModule databaseModule = (ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule);
            IDatabase systemDatabase = databaseModule.GetDatabaseContainer().GetDatabase(SystemeConstants.SystemDatabaseName);
            ITable profilesPrivilegesTable = systemDatabase.GetTable(SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            ITable tablePrivilegesTable = systemDatabase.GetTable(SystemeConstants.PrivilegesTableName);
            ITable adminProfileTable = systemDatabase.GetTable(SystemeConstants.NoRemovableProfilesTableName);
            IEnumerator<Row> adminProfilesrowEnumerator = adminProfileTable.GetRowEnumerator();
            IEnumerator<Row> tablePrivilegesRowEnumerator = tablePrivilegesTable.GetRowEnumerator();
            Row row;
            while (adminProfilesrowEnumerator.MoveNext())
            {
                while (tablePrivilegesRowEnumerator.MoveNext())
                {
                    row = profilesPrivilegesTable.CreateRowDefinition();
                    row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName).data = database.databaseName;
                    row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName).data = table.tableName;//If i dont use an array with lamdas with this is because of NO LIAR LA COSA MAS
                    row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName).data = adminProfilesrowEnumerator.Current.GetCell(SystemeConstants.ProfileNameColumn).data;
                    row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName).data = tablePrivilegesRowEnumerator.Current.GetCell(SystemeConstants.PrivilegesPrivilegeNameColumnName).data;
                    profilesPrivilegesTable.AddRow(row);
                }
                tablePrivilegesRowEnumerator.Reset();
            }
        }

        public void ActToRemove(IDatabase database)
        {
            Delete delete = new Delete(((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabaseContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnTablesTableName;
            delete.whereClause.AddCritery(new Tuple<string, string>(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, database.databaseName), Operator.equal);
            delete.Execute();

            delete = new Delete(((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabaseContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName;
            delete.whereClause.AddCritery(new Tuple<string, string>(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName, database.databaseName), Operator.equal);
            delete.Execute();
        }

        public void ActToRemove(IDatabase database, ITable table)
        {
            Delete delete = new Delete(((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabaseContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnTablesTableName;
            delete.whereClause.AddCritery(new Tuple<string, string>(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, database.databaseName), Operator.equal);
            delete.whereClause.AddCritery(new Tuple<string, string>(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, table.tableName), Operator.equal);
            delete.Execute();
        }

        public string GetModuleKey()
        {
            return SystemeConstants.SystemePrivilegeModule;
        }

        public bool CheckProfileTablePrivileges(string username, string databaseName, string tableName, string privilegeType)
        {
            bool b = true;

            return b;
        }

        public void TableModified(IDatabase database, ITable table)
        {
            
        }

        public void AcoplateTheModule()
        {
            
        }

        public bool IsAcoplated()
        {
            return true;
        }

        public ISysteme GetSysteme()
        {
            return this.systeme;
        }
    }
}
