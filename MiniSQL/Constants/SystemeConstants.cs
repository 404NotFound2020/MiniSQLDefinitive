using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Constants
{
    public class SystemeConstants
    {
        public const string DefaultDatabaseName = "default";
        public const bool AllowedToNotSpecifyDatabaseInQuerys = true; //Nothing
        public const string SystemDatabaseName = "system";
        //Users table
        public const string UsersTableName = "users";
        public const string UsersNameColumnName = "username";
        public const string UsersNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string UsersPasswordColumnName = "password";
        public const string UsersPasswordColumnType = TypesKeyConstants.StringTypeKey;
        //Profiles table
        public const string ProfilesTableName = "profiles";
        public const string NoRemovableProfilesTableName = "defaultProfiles"; 
        public const string ProfileNameColumn = "profileName";
        public const string ProfileNameColumnType = TypesKeyConstants.StringTypeKey;
        //Special user and profiles tables
        public const string NoRemovableUsersTableName = "defaultUsers";
        public const string AdminUser = "admin";
        public const string AdminPassword = "admin";
        public const string DefaultProfile = "admin";
        public const string UserProfilesTableName = "userProfiles";
        public const string NoRemovableUserProfilesTableName = "defaultUsersProfiles";
        public const string UserProfilesUsernameColumnName = UsersNameColumnName;
        public const string UserProfilesUsernameColumnType = UsersNameColumnType;
        public const string UserProfilesProfileColumnName = ProfileNameColumn;
        public const string UserProfilesProfileColumnType = ProfileNameColumnType;
        //Table privileges table
        public const string PrivilegesTableName = "tablePrivileges";
        public const string PrivilegesPrivilegeNameColumnName = "privilegeName";
        public const string PriviliegesPrivilegeColumnType = TypesKeyConstants.StringTypeKey;
        public const string InsertPrivilegeName = "INSERT";
        public const string DeletePrivilegeName = "DELETE";
        public const string UpdatePrivilegeName = "UPDATE";
        public const string SelectPrivilegeName = "SELECT";
        //Databases privileges table
        public const string DatabasesPrivilegesTableName = "databasePrivileges";
        public const string DatabasesPrivilegesPrivilegeNameColumnName = "privilegeName";
        public const string DatabasesPrivilegesPrivilegeColumnType = TypesKeyConstants.StringTypeKey;
        public const string CreatePrivilegeName = "CREATE";
        public const string DropPrivilegeName = "DROP";
        //Profile privileges on tables table
        public const string PrivilegesOfProfilesOnTablesTableName = "profilesTablePrivileges";        
        public const string PrivilegesOfProfilesOnTablesProfileColumnName = "profile";
        public const string PrivilegesOfProfilesOnTablesProfileColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnTablesDatabaseNameColumnName = "databaseName";
        public const string PrivilegesOfProfilesOnTablesDatabaseNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnTablesTableNameColumnName = "tableName";
        public const string PrivilegesOfProfilesOnTablesTableNameColumnNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnTablesPrivilegeColumnName = "privilege";
        public const string PrivilegesOfProfilesOnTablesPrivilegeColumnType = TypesKeyConstants.StringTypeKey;
        //Profile privileges on databases table
        public const string PrivilegesOfProfilesOnDatabasesTableName = "profilesDatabasesPrivileges";
        public const string PrivilegesOfProfilesOnDatabasesProfileColumnName = "profile";
        public const string PrivilegesOfProfilesOnDatabasesProfileColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName = "databaseName";
        public const string PrivilegesOfProfilesOnDatabasesDatabaseNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnDatabasesPrivilegeColumnName = "privilege";
        public const string PrivilegesOfProfilesOnDatabasesPrivilegeColumnType = TypesKeyConstants.StringTypeKey;
        //Modules key
        public const string SystemeDatabaseModule = "SystemeDatabaseModule";
        public const string SystemePrivilegeModule = "SystemePrivilegeModule";
        public const string SystemeDataInicializationModule = "SystemeDataInicializationModule";


    }
}
