using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public static class DefaultDataConstructor
    {
        public static Database CreateSystemDatabase() 
        {
            Database systemDatabase = new Database(SystemeConstants.SystemDatabaseName);
            systemDatabase.AddTable(CreateUsersTable());
            systemDatabase.AddTable(CreateProfilesTable());
            systemDatabase.AddTable(CreateUserProfilesTable(systemDatabase.GetTable(SystemeConstants.UsersTableName), systemDatabase.GetTable(SystemeConstants.ProfilesTableName)));
            systemDatabase.AddTable(CreateNoRemovableUsersTable(systemDatabase.GetTable(SystemeConstants.UsersTableName)));
            systemDatabase.AddTable(CreateNoRemovableProfilesTable(systemDatabase.GetTable(SystemeConstants.ProfilesTableName)));
            systemDatabase.AddTable(CreateNoRemovableUserProfilesTable(systemDatabase.GetTable(SystemeConstants.UserProfilesTableName)));
            systemDatabase.AddTable(CreatePrivilegesTable());
            systemDatabase.AddTable(CreatePrivilegesOfProfilesTable(systemDatabase.GetTable(SystemeConstants.ProfilesTableName), systemDatabase.GetTable(SystemeConstants.PrivilegesTableName)));
            systemDatabase.AddTable(CreateDatabasesPrivilegesTable());
            systemDatabase.AddTable(CreatePrivilegesOfProfilesInDatabasesTable(systemDatabase.GetTable(SystemeConstants.ProfilesTableName), systemDatabase.GetTable(SystemeConstants.DatabasesPrivilegesTableName)));
            return systemDatabase;
        }

        public static Table CreateUsersTable()
        {
            Table table = new Table(SystemeConstants.UsersTableName);
            DataTypesFactory dataTypesFactory = DataTypesFactory.GetDataTypesFactory();
            table.AddColumn(new Column(SystemeConstants.UsersNameColumnName, dataTypesFactory.GetDataType(SystemeConstants.UsersNameColumnType)));
            table.AddColumn(new Column(SystemeConstants.UsersPasswordColumnName, dataTypesFactory.GetDataType(SystemeConstants.UsersPasswordColumnType)));
            table.primaryKey.AddKey(table.GetColumn(SystemeConstants.UsersNameColumnName));
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UsersNameColumnName).data = SystemeConstants.AdminUser;
            row.GetCell(SystemeConstants.UsersPasswordColumnName).data = SystemeConstants.AdminPassword;
            table.AddRow(row);
            return table;
        }

        public static Table CreateNoRemovableUsersTable(ITable usersTable)
        {
            Table table = new Table(SystemeConstants.NoRemovableUsersTableName);            
            table.AddColumn(new Column(SystemeConstants.UsersNameColumnName, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.UsersNameColumnType)));
            table.primaryKey.AddKey(table.GetColumn(SystemeConstants.UsersNameColumnName));
            table.foreignKey.AddForeignKey(table.GetColumn(SystemeConstants.UsersNameColumnName), usersTable.GetColumn(SystemeConstants.UsersNameColumnName));                
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UsersNameColumnName).data = SystemeConstants.AdminUser;
            table.AddRow(row);           
            return table;
        }

        public static Table CreateProfilesTable()
        {
            Table table = new Table(SystemeConstants.ProfilesTableName);
            table.AddColumn(new Column(SystemeConstants.ProfileNameColumn, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.ProfileNameColumnType)));
            table.primaryKey.AddKey(table.GetColumn(SystemeConstants.ProfileNameColumn));
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.ProfileNameColumn).data = SystemeConstants.DefaultProfile;
            table.AddRow(row);
            return table;
        }

        public static Table CreateNoRemovableProfilesTable(ITable profilesTable)
        {
            Table table = new Table(SystemeConstants.NoRemovableProfilesTableName);
            table.AddColumn(new Column(SystemeConstants.ProfileNameColumn, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.ProfileNameColumnType)));
            table.primaryKey.AddKey(table.GetColumn(SystemeConstants.ProfileNameColumn));
            table.foreignKey.AddForeignKey(table.GetColumn(SystemeConstants.ProfileNameColumn), profilesTable.GetColumn(SystemeConstants.ProfileNameColumn));
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.ProfileNameColumn).data = SystemeConstants.DefaultProfile;
            table.AddRow(row);
            return table;
        }

        public static Table CreateUserProfilesTable(ITable userTable, ITable profileTable) 
        {
            Table table = CreateUserProfilesTable(SystemeConstants.UserProfilesTableName);
            table.foreignKey.AddForeignKey(table.GetColumn(SystemeConstants.UserProfilesUsernameColumnName), userTable.GetColumn(SystemeConstants.UsersNameColumnName));
            table.foreignKey.AddForeignKey(table.GetColumn(SystemeConstants.UserProfilesProfileColumnName), profileTable.GetColumn(SystemeConstants.ProfileNameColumn));
            return table;
        }

        public static Table CreateNoRemovableUserProfilesTable(ITable userProfilesTable)
        {
            Table table = CreateUserProfilesTable(SystemeConstants.NoRemovableUserProfilesTableName);
            table.foreignKey.AddForeignKey(table.GetColumn(SystemeConstants.UserProfilesUsernameColumnName), userProfilesTable.GetColumn(SystemeConstants.UserProfilesUsernameColumnName));
            table.foreignKey.AddForeignKey(table.GetColumn(SystemeConstants.UserProfilesProfileColumnName), userProfilesTable.GetColumn(SystemeConstants.UserProfilesProfileColumnName));
            return table;
        }

        private static Table CreateUserProfilesTable(string tableName) {
            Table table = new Table(tableName);
            DataTypesFactory dataTypesFactory = DataTypesFactory.GetDataTypesFactory();
            Column userNameColumn = new Column(SystemeConstants.UserProfilesUsernameColumnName, dataTypesFactory.GetDataType(SystemeConstants.UserProfilesUsernameColumnType));
            Column profileNameColumn = new Column(SystemeConstants.UserProfilesProfileColumnName, dataTypesFactory.GetDataType(SystemeConstants.UserProfilesProfileColumnType));
            table.AddColumn(userNameColumn);
            table.AddColumn(profileNameColumn);
            table.primaryKey.AddKey(userNameColumn);
            table.primaryKey.AddKey(profileNameColumn);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UserProfilesUsernameColumnName).data = SystemeConstants.AdminUser;
            row.GetCell(SystemeConstants.ProfileNameColumn).data = SystemeConstants.DefaultProfile;
            table.AddRow(row);
            return table;
        }

        public static Table CreatePrivilegesTable()
        {
            Table table = new Table(SystemeConstants.PrivilegesTableName);
            Column privilegesNameColumn = new Column(SystemeConstants.PrivilegesPrivilegeNameColumnName, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.PriviliegesPrivilegeColumnType));
            table.AddColumn(privilegesNameColumn);
            table.primaryKey.AddKey(privilegesNameColumn);
            string[] privilegesList = new string[] { SystemeConstants.InsertPrivilegeName, SystemeConstants.SelectPrivilegeName, SystemeConstants.DeletePrivilegeName, SystemeConstants.UpdatePrivilegeName };//XD esto con 100 tipos de privilegios seria unas risas (obviamente se haria de otra manera en ese caso)
            Row row;
            for(int i = 0; i < privilegesList.Length; i++)
            {
                row = table.CreateRowDefinition();
                row.GetCell(SystemeConstants.PrivilegesPrivilegeNameColumnName).data = privilegesList[i];
                table.AddRow(row);
            }
            return table;
        }
        //No se reutiliza porque esto de las tablas mejor hacerlas en metodos independientes, que luego cambiar cualquier cosa, las tablas ya no son tan parecidas y toca volver a separarlo
        public static Table CreateDatabasesPrivilegesTable()
        {
            Table table = new Table(SystemeConstants.DatabasesPrivilegesTableName);
            Column privilegesNameColumn = new Column(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.DatabasesPrivilegesPrivilegeColumnType));
            table.AddColumn(privilegesNameColumn);
            table.primaryKey.AddKey(privilegesNameColumn);
            string[] privilegesList = new string[] { SystemeConstants.CreatePrivilegeName, SystemeConstants.DropPrivilegeName };
            Row row;
            for(int i = 0; i < privilegesList.Length; i++)
            {
                row = table.CreateRowDefinition();
                row.GetCell(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName).data = privilegesList[i];
                table.AddRow(row);
            }
            return table;
        }

        public static Table CreatePrivilegesOfProfilesTable(ITable profilesTable, ITable privilegesTable)
        {
            Table table = new Table(SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            DataTypesFactory dataTypesFactory = DataTypesFactory.GetDataTypesFactory();
            Column profilesNameColumn = new Column(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName, dataTypesFactory.GetDataType(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnType));
            Column databaseNameColumn = new Column(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, dataTypesFactory.GetDataType(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnType));
            Column tableNameColumn = new Column(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, dataTypesFactory.GetDataType(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnNameColumnType));
            Column privilegeNameColumn = new Column(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName, dataTypesFactory.GetDataType(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnType));
            table.AddColumn(profilesNameColumn);
            table.AddColumn(databaseNameColumn);           
            table.AddColumn(tableNameColumn);
            table.AddColumn(privilegeNameColumn);
            table.primaryKey.AddKey(profilesNameColumn);
            table.primaryKey.AddKey(databaseNameColumn);//Se podria crear una tabla para guardar datos de las tablas y otras para los datos de las databases, pero bueno, no me ha dado la gana de hacerlo.
            table.primaryKey.AddKey(tableNameColumn);
            table.primaryKey.AddKey(privilegeNameColumn);
            table.foreignKey.AddForeignKey(profilesNameColumn, profilesTable.GetColumn(SystemeConstants.UserProfilesProfileColumnName));
            table.foreignKey.AddForeignKey(privilegeNameColumn, privilegesTable.GetColumn(SystemeConstants.PrivilegesPrivilegeNameColumnName));
            return table;
        }

        public static Table CreatePrivilegesOfProfilesInDatabasesTable(ITable profilesTable, ITable privilegesTable)
        {
            Table table = new Table(SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            DataTypesFactory dataTypesFactory = DataTypesFactory.GetDataTypesFactory();
            Column profilesNameColumn = new Column(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName, dataTypesFactory.GetDataType(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnType));
            Column databaseNameColumn = new Column(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName, dataTypesFactory.GetDataType(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnType));
            Column privilegeNameColumn = new Column(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName, dataTypesFactory.GetDataType(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnType));
            table.AddColumn(profilesNameColumn);
            table.AddColumn(databaseNameColumn);
            table.AddColumn(privilegeNameColumn);
            table.primaryKey.AddKey(profilesNameColumn);
            table.primaryKey.AddKey(databaseNameColumn);
            table.primaryKey.AddKey(privilegeNameColumn);
            table.foreignKey.AddForeignKey(profilesNameColumn, profilesTable.GetColumn(SystemeConstants.UserProfilesProfileColumnName));
            table.foreignKey.AddForeignKey(privilegeNameColumn, privilegesTable.GetColumn(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName));
            return table;
        }

    }
}
