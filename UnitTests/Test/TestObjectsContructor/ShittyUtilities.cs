using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    public class ShittyUtilities
    {

        public static string GetDoenstExistProfileName(IDatabaseContainer databaseContainer)
        {
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName);
            return GenerateADoenstExistNewValue(table.GetColumn(SystemeConstants.ProfileNameColumn));
        }

        public static string GetDoesntExistPrivilegeName(IDatabaseContainer databaseContainer)
        {
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.PrivilegesTableName);
            return GenerateADoenstExistNewValue(table.GetColumn(SystemeConstants.PrivilegesPrivilegeNameColumnName));
        }

        public static string GetDoesntExistDatabasePrivilegeName(IDatabaseContainer databaseContainer)
        {
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.DatabasesPrivilegesTableName);
            return GenerateADoenstExistNewValue(table.GetColumn(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName));
        }

        public static string GetDoesntExistUserName(IDatabaseContainer databaseContainer)
        {
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.UsersTableName);
            return GenerateADoenstExistNewValue(table.GetColumn(SystemeConstants.UsersNameColumnName));
        }

        public static string GetDoenstExistDatabaseName(IDatabaseContainer databaseContainer)
        {
            string databaseName = VariousFunctions.GenerateRandomString(12);
            while (databaseContainer.ExistDatabase(databaseName)) databaseName = VariousFunctions.GenerateRandomString(12);
            return databaseName;
        }

        public static string GetDoenstExistTableName(IDatabase database)
        {
            string tableName = VariousFunctions.GenerateRandomString(12);
            while (database.ExistTable(tableName)) tableName = VariousFunctions.GenerateRandomString(12);
            return tableName;
        }

        public static string GenerateADoenstExistNewValue(Column column)
        {
            string value = VariousFunctions.GenerateRandomString(8);
            while (column.ExistCells(value)) value = VariousFunctions.GenerateRandomString(8);
            return value;
        }


    }
}
