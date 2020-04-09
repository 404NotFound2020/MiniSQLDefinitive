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
        public const bool AllowedToNotSpecifyDatabaseInQuerys = true;
        public const string SystemDatabaseName = "system";
        public const string UsersTableName = "users";
        public const string UsersNameColumnName = "username";
        public const string UsersNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string UsersPasswordColumnName = "password";
        public const string UsersPasswordColumnType = TypesKeyConstants.StringTypeKey;

        public const string ProfilesTableName = "profiles";
        public const string NoRemovableProfilesTableName = "defaultProfiles"; 
        public const string ProfileNameColumn = "profileName";
        public const string ProfileNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string NoRemovableUsersTableName = "defaultUsers";
        public const string AdminUser = "admin";
        public const string AdminPassword = "admin";
        public const string DefaultProfile = "admin";
        public const string UserProfilesTableName = "userProfiles";
        public const string NoRemovableUserProfilesTableName = "defaultUsersProfiles";
        public const string UserProfilesUsernameColumnName = UsersNameColumnName;
        public const string UserProfilesUsernameColumnType = TypesKeyConstants.StringTypeKey;
        public const string UserProfilesProfileColumnName = ProfileNameColumn;
        public const string UserProfilesProfileColumnType = TypesKeyConstants.StringTypeKey;

        public const string PrivilegesTableName = "privileges";
        public const string PrivilegesPrivilegeNameColumnName = "privilegeName";
        public const string PriviliegesPrivilegeColumnType = TypesKeyConstants.StringTypeKey;
        public const string InsertPrivilegeName = "INSERT";
        public const string DeletePrivilegeName = "DELETE";
        public const string UpdatePrivilegeName = "UPDATE";
        public const string SelectPrivilegeName = "SELECT";

        public const string PrivilegesOfProfilesOnTablesTableName = "profilesPrivileges";        
        public const string PrivilegesOfProfilesOnTablesProfileColumnName = "profile";
        public const string PrivilegesOfProfilesOnTablesProfileColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnTablesDatabaseNameColumnName = "databaseName";
        public const string PrivilegesOfProfilesOnTablesDatabaseNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnTablesTableNameColumnName = "tableName";
        public const string PrivilegesOfProfilesOnTablesTableNameColumnNameColumnType = TypesKeyConstants.StringTypeKey;
        public const string PrivilegesOfProfilesOnTablesPrivilegeColumnName = "privilege";
        public const string PrivilegesOfProfilesOnTablesPrivilegeColumnType = TypesKeyConstants.StringTypeKey;


    }
}
