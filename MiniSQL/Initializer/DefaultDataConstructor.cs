using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
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
            return systemDatabase;
        }

        public static Table CreateUsersTable() 
        {
            Table table = new Table(SystemeConstants.UsersTableName);
            table.AddColumn(new Column(SystemeConstants.UsersNameColumnName, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.UsersNameColumnType)));
            table.AddColumn(new Column(SystemeConstants.UsersPasswordColumnName, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.UsersPasswordColumnType)));
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UsersNameColumnName).data = SystemeConstants.AdminUser;
            row.GetCell(SystemeConstants.UsersPasswordColumnName).data = SystemeConstants.AdminPassword;
            table.AddRow(row);
            return table;
        }

        public static Table CreateProfilesTable() 
        {
            Table table = new Table(SystemeConstants.ProfilesTableName);
            table.AddColumn(new Column(SystemeConstants.ProfileNameColumn, DataTypesFactory.GetDataTypesFactory().GetDataType(SystemeConstants.ProfileNameColumnType)));
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.ProfileNameColumn).data = SystemeConstants.DefaultProfile;
            table.AddRow(row);
            return table;
        }
    }
}
