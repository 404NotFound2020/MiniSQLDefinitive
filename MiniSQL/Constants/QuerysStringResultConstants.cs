using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Constants
{
    public class QuerysStringResultConstants
    {

        public const string createDatabaseSuccess = "Database created";
        public const string InsertSuccess = "Tuple added";
        public const string Error = "ERROR: ";
        public const string TooMuchValues = Error + "Too much values";
        public const string TheDatabaseWasDeleted = "The database was deleted";
        public const string TheDatabaseWasCreated = "The database was created";

        public static string WhereClauseColumnDoensExist(string columnName) 
        {
            return "The column " + columnName + " which was indicated in where clause doenst exist";
        }

        public static string WhereClauseColumnDataTypeError(string columnName)
        {
            return "The column " + columnName + " which was indicated in where clause and the value dont match";
        }

        public static string SelectedColumnDoenstExistError(string columnName) 
        {
            return "The column " + columnName + " which was selected, doesnt exist"; 
        }

        public static string ColumnsAndDataTypesError(string columnName, string dataType) 
        {
            return "The data of the column " + columnName + " is " + dataType;
        }

        public static string TableDoensExist(string databaseName, string tableName) 
        {
            return "The table " + tableName + " doesnt exist in the " + databaseName + " database";
        }

        public static string DatabaseDoesntExist(string databaseName) 
        {
            return "The database " + databaseName + " doesnt exit";
        }

        public static string TheTableAlreadyExists(string tableName) 
        {
            return "The table " + tableName + " already exist";
        }

        public static string TheColumnAlreadyDefined(string columnName) 
        {
            return "The column " + columnName + " already defined";
        }

        public static string TableWasCreated(string database, string tablename) 
        {
            return "Table " + tablename + " was sucefully created in database " + database;
        }

        public static string TableSucesfullyDeleted(string tableName) 
        {
            return "The table " + tableName + " was sucesfully deleted";
        }

        public static string DeletedRow(int rowCount) {
            return rowCount + " rows deleted";
        }

        public static string TheDatabaseExist(string databaseName) {
            return "The database " + databaseName + " already exist";
        }
  
    }
}
