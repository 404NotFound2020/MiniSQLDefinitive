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
            Delete delete = new Delete(((ISystemeDatabaseModule) this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabaseContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnTablesTableName;
            delete.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, database.databaseName, Operator.equal);
            delete.Execute();

            delete = new Delete(((ISystemeDatabaseModule) this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabaseContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName;
            delete.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName, database.databaseName, Operator.equal);
            delete.Execute();
        }

        public void ActToRemove(IDatabase database, ITable table)
        {
            Delete delete = new Delete(((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabaseContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnTablesTableName;
            delete.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, database.databaseName, Operator.equal);
            delete.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, table.tableName, Operator.equal);
            delete.Execute();
        }

        public string GetModuleKey()
        {
            return SystemeConstants.SystemePrivilegeModule;
        }

        public bool CheckProfileTablePrivileges(string username, string databaseName, string tableName, string privilegeType)
        {
            bool b = false;
            IDatabase systemDatabase = ((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabase(SystemeConstants.SystemDatabaseName);
            Column privilegesOfProfileColumn = systemDatabase.GetTable(SystemeConstants.PrivilegesOfProfilesOnTablesTableName).GetColumn(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName);
            if (privilegesOfProfileColumn.ExistCells(privilegeType))
            {
                List<Cell> cellList = privilegesOfProfileColumn.GetCells(privilegeType);
                Dictionary<string, string> keysAndValues = new Dictionary<string, string>();
                keysAndValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName, this.GetUserProfile(username, systemDatabase));
                keysAndValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, databaseName);
                keysAndValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, tableName);
                b = this.FastSearch(cellList, keysAndValues, 0, b);
            }
            return b;
        }

        public bool CheckProfileDatabasePrivileges(string username, string databaseName, string privilegeType)
        {
            bool b = false;
            IDatabase systemDatabase = ((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabase(SystemeConstants.SystemDatabaseName);
            Column privilegesOfProfileColumn = systemDatabase.GetTable(SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName).GetColumn(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName);
            if (privilegesOfProfileColumn.ExistCells(privilegeType))
            {
                List<Cell> cellList = privilegesOfProfileColumn.GetCells(privilegeType);
                Dictionary<string, string> keysAndValues = new Dictionary<string, string>();
                keysAndValues.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName, this.GetUserProfile(username, systemDatabase));
                keysAndValues.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName, databaseName);
                b = this.FastSearch(cellList, keysAndValues, 0, b);
            }
            return b;
        }

        public bool CheckIsAutorizedToExecuteSecurityQuery(string username)
        {
            IDatabase systemDatabase = ((ISystemeDatabaseModule) this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabase(SystemeConstants.SystemDatabaseName);
            return systemDatabase.GetTable(SystemeConstants.NoRemovableProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn).ExistCells(this.GetUserProfile(username, systemDatabase));
        }

        private string GetUserProfile(string username, IDatabase systemDatabase)
        {
            return systemDatabase.GetTable(SystemeConstants.UsersTableName).GetColumn(SystemeConstants.UsersNameColumnName).GetCells(username)[0].row.GetCell(SystemeConstants.UsersProfileColumnName).data;
        }

        private bool FastSearch(List<Cell> cellsRows, Dictionary<string, string> keyValuePairs, int numOfRow, bool b) {
            if (!(numOfRow == cellsRows.Count) && !b)
            {
                Row row = cellsRows[numOfRow].row;
                IEnumerator<string> keysEnum = keyValuePairs.Keys.GetEnumerator();
                bool c = true;
                while (keysEnum.MoveNext() && c) c = row.GetCell(keysEnum.Current).data.Equals(keyValuePairs[keysEnum.Current]);
                keysEnum.Dispose();
                b = FastSearch(cellsRows, keyValuePairs, numOfRow + 1, c);
            } 
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
