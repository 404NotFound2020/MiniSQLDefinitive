using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Constants
{
    public class RequestAndRegexConstants
    {
        public const string selectQueryIdentificator = "SELECT";
        public const string insertQueryIdentificator = "INSERT";
        public const string dropTableQueryIdentificator = "DROP TABLE";
        public const string deleteQueryIdentificator = "DELETE";
        public const string updateQueryIdentificator = "UPDATE";
        public const string createTableQueryIdentificator = "CREATE TABLE";
        public const string createDatabaseQueryIdentificator = "CREATE DATABASE";
        public const string dropDatabaseQueryIdentificator = "DROP DATABASE";
        public const string createSecurityProfileQueryIdentificator = "CREATE SECURITY PROFILE";
        public const string dropSecurityProfileQueryIdentificator = "DROP SECURITY PROFILE";
        public const string deleteUserQueryIdentificator = "DELETE USER";
        public const string createUserQueryIdentificator = "ADD USER";
        public const string grantDatabasePrivilegeQueryIdentificator = "GRANT DATABASE PRIVILEGE";
        public const string revokeDatabasePrivilegeQueryIdentificator = "REVOKE DATABASE PRIVILEGE";
        public const string grantTablePrivilegeQueryIdentificator = "GRANT";
        public const string revokeTablePrivilegeQueryIdentificator = "REVOKE";
        public const string loginQueryIdentificator = "LOGIN";

        public static string queryTagName = "query";
        public static string databaseTagName = "database";
        public static string tableTagName = "table";
        public static string selectedColumnTagName = "selectedColumn";
        public static string toEvaluateColumnTagName = "toEvalColumn";
        public static string valueTagName = "value";
        public static string updatedColumnTagName = "updatedColumn";
        public static string updatedValueTagName = "toValue";
        public static string operatorTagName = "operator";
        public static string evalValueTagName = "evalValue";
        public static string columnTagName = "column";
        public static string columnTypeTagName = "columnType";
        public static string usernameTagName = "username";
        public static string passwordTagName = "password";
        public static string privilegeTag = "privilege";
        public static string securityProfileTag = "securityProfile";

        public static string queryGroup = "?<" + queryTagName + ">";
        public static string databaseGroup = "?<" + databaseTagName + ">";
        public static string tableGroup = "?<" + tableTagName + ">";
        public static string selectedColumnGroup = "?<" + selectedColumnTagName + ">";
        public static string toEvaluateColumnGroup = "?<" + toEvaluateColumnTagName + ">";
        public static string valueGroup = "?<" + valueTagName + ">";
        public static string updatedColumnGroup = "?<" + updatedColumnTagName + ">";
        public static string updatedvalueGroup = "?<" + updatedValueTagName + ">";
        public static string operatorGroup = "?<" + operatorTagName + ">";
        public static string evalValueGroup = "?<" + evalValueTagName + ">";
        public static string columnGroup = "?<" + columnTagName + ">";
        public static string columnTypeGroup = "?<" + columnTypeTagName + ">";
        public static string usernameGroup = "?<" + usernameTagName + ">";
        public static string passwordGroup = "?<" + passwordTagName + ">";
        public static string privilegeGroup = "?<" + privilegeTag + ">";
        public static string securityProfileGroup = "?<" + securityProfileTag + ">";


        public static string columnsTypes = TypesKeyConstants.IntTypeKey + "|" + TypesKeyConstants.DoubleTypeKey + "|" + TypesKeyConstants.StringTypeKey;
        public static string operators = "[" + OperatorKeys.EqualKey + OperatorKeys.HigherKey + OperatorKeys.LessKey + "]";
        public static string NAINCG = "[^\\* ,<=>\\(\\)]";
        public static string databaseRegexPart = "(?:(" + databaseGroup + NAINCG + "+)\\.)" + (SystemeConstants.AllowedToNotSpecifyDatabaseInQuerys ? "?" : "");
        public static string valuesRegexPart = "(?:-?[0-9]+(?:\\.[0-9]+)?)|(?:'[^']+')";
        public static string insertValueRegexPart = "(" + valueGroup + "-?[0-9]+(?:\\.[0-9]+)?)|'(" + valueGroup + "[^']+)'";
        public static string updateValueRegexPart = "(" + updatedvalueGroup + "-?[0-9]+(?:\\.[0-9]+)?)|'(" + updatedvalueGroup + "[^']+)'";
        public static string whereValueRegexPart = "(" + evalValueGroup + "-?[0-9]+(?:\\.[0-9]+)?)|'(" + evalValueGroup + "[^']+)'";
        public static string wherePatern = "WHERE (" + toEvaluateColumnGroup + NAINCG + "+)(" + operatorGroup + operators + ")(?:" + whereValueRegexPart + ")";
        public static string fromPattern = "FROM " + databaseRegexPart + "(" + tableGroup + NAINCG + "+)(?: " + wherePatern + ")";
        public static string selectPattern = "^(" + queryGroup + selectQueryIdentificator + ") (?:(" + selectedColumnGroup + "\\*)|(" + selectedColumnGroup + NAINCG + "+)(?:, (" + selectedColumnGroup + NAINCG + "+))*) " + fromPattern + "?;$";
        public static string insertPattern = "^(" + queryGroup + insertQueryIdentificator + ") INTO " + databaseRegexPart + "(" + tableGroup + NAINCG + "+) VALUES \\((?:" + insertValueRegexPart + ")(?:, (?:" + insertValueRegexPart + "))*\\);$";
        public static string dropPattern = "^(" + queryGroup + dropTableQueryIdentificator + ") " + databaseRegexPart + "(" + tableGroup + NAINCG + "+);$";
        public static string deletePattern = "^(" + queryGroup + deleteQueryIdentificator + ") " + fromPattern + "?;$";
        public static string updatePattern = "^(" + queryGroup + updateQueryIdentificator + ") " + databaseRegexPart + "(" + tableGroup + NAINCG + "+) SET (" + updatedColumnGroup + NAINCG + "+)=(?:" + updateValueRegexPart + ")(?:, (" + updatedColumnGroup + NAINCG + "+)=(?:" + updateValueRegexPart + "))* " + wherePatern + ";$";
        public static string createPattern = "^(" + queryGroup + createTableQueryIdentificator + ") " + databaseRegexPart + "(" + tableGroup + NAINCG + "+) \\((" + columnGroup + NAINCG + "+) (" + columnTypeGroup + columnsTypes + ")(?:, (" + columnGroup + NAINCG + "+) (" + columnTypeGroup + columnsTypes + "))*\\);$";
        public static string createDatabasePattern = "^(" + queryGroup + createDatabaseQueryIdentificator + ") (" + databaseGroup + NAINCG + "+);";
        public static string dropDatabasePattern = "^(" + queryGroup + dropDatabaseQueryIdentificator + ") (" + databaseGroup + NAINCG + "+);";
        public static string createSecurityProfile = "^(" + queryGroup + createSecurityProfileQueryIdentificator + ") '(" + valueGroup + "[^']+)';$";
        public static string dropSecurityProfile = "^(" + queryGroup + dropSecurityProfileQueryIdentificator + ") '(" + valueGroup + "[^']+)';$";
        public static string deleteUser = "^(" + queryGroup + deleteUserQueryIdentificator + ") '(" + usernameGroup + "[^']+)';$";
        public static string createUser = "^(" + queryGroup + createUserQueryIdentificator + ") \\('(" + usernameGroup + "[^']+)', '(" + passwordGroup + "[^']+)', '(" + securityProfileGroup + "[^']+)'\\);$";
        public static string grantDatabasePrivilege = "^(" + queryGroup + grantDatabasePrivilegeQueryIdentificator + ") '(" + privilegeGroup + "[^']+)' ON (" + databaseGroup + NAINCG + "+) TO '(" + securityProfileGroup + "[^']+)';$"; //Notice that database string is not like a others string
        public static string revokeDatabasePrivilege = "^(" + queryGroup + revokeDatabasePrivilegeQueryIdentificator + ") '(" + privilegeGroup + "[^']+)' ON (" + databaseGroup + NAINCG + "+) TO '(" + securityProfileGroup + "[^']+)';$";
        public static string grantTablePrivilege = "^(" + queryGroup + grantTablePrivilegeQueryIdentificator + ") '(" + privilegeGroup + "[^']+)' ON " + databaseRegexPart + "(" + tableGroup + NAINCG + "+) TO '(" + securityProfileGroup + "[^']+)';$";
        public static string revokeTablePrivilege = "^(" + queryGroup + revokeTablePrivilegeQueryIdentificator + ") '(" + privilegeGroup + "[^']+)' ON " + databaseRegexPart + "(" + tableGroup + NAINCG + "+) TO '(" + securityProfileGroup + "[^']+)';$";
        public static string login = "^(" + queryGroup + loginQueryIdentificator + ") '(" + usernameGroup + "[^']+)', '(" + passwordGroup + "[^']+)'" + ";$";

    }
}
