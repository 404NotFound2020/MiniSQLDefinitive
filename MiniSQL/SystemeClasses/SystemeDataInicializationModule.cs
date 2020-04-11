using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SystemeClasses
{
    public class SystemeDataInicializationModule : ISystemeDataInicializationModule
    {
        private ISysteme systeme;
        private bool isAcoplated;
        private static SystemeDataInicializationModule systemeDataInicializationModule;
       
        private SystemeDataInicializationModule()
        {
            this.isAcoplated = false;
        }

        public static SystemeDataInicializationModule GetSystemeDataInicializationModule()
        {
            if (systemeDataInicializationModule == null) systemeDataInicializationModule = new SystemeDataInicializationModule();
            return systemeDataInicializationModule;
        }

        public void SetSysteme(ISysteme system)
        {
            this.systeme = system;
        }

        public string GetModuleKey()
        {
            return SystemeConstants.SystemeDataInicializationModule;
        }
       
        public void AcoplateTheModule()
        {
            if (!this.isAcoplated)
            {
                ISystemeDatabaseModule databaseModule = (ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule);
                databaseModule.AcoplateTheModule();
                this.CreateSystemDatabases(databaseModule);
                this.CreateDefaultDatabase(databaseModule);
                this.isAcoplated = true;
            }
        }
        private void CreateSystemDatabases(ISystemeDatabaseModule databaseModule)
        {
            if (!databaseModule.ExistDatabase(SystemeConstants.SystemDatabaseName)) databaseModule.AddDatabase(DefaultDataConstructor.CreateSystemDatabase());
            else
            {
                IDatabase database = databaseModule.GetDatabase(SystemeConstants.SystemDatabaseName);
                if (!database.ExistTable(SystemeConstants.UsersTableName)) database.AddTable(DefaultDataConstructor.CreateUsersTable());
                if (!database.ExistTable(SystemeConstants.ProfilesTableName)) database.AddTable(DefaultDataConstructor.CreateProfilesTable());
                if (!database.ExistTable(SystemeConstants.NoRemovableUsersTableName)) database.AddTable(DefaultDataConstructor.CreateNoRemovableUsersTable(database.GetTable(SystemeConstants.UsersTableName)));
                if (!database.ExistTable(SystemeConstants.NoRemovableProfilesTableName)) database.AddTable(DefaultDataConstructor.CreateNoRemovableProfilesTable(database.GetTable(SystemeConstants.ProfilesTableName)));
                if (!database.ExistTable(SystemeConstants.UserProfilesTableName)) database.AddTable(DefaultDataConstructor.CreateUserProfilesTable(database.GetTable(SystemeConstants.UsersTableName), database.GetTable(SystemeConstants.ProfilesTableName)));
                if (!database.ExistTable(SystemeConstants.NoRemovableUserProfilesTableName)) database.AddTable(DefaultDataConstructor.CreateNoRemovableUserProfilesTable(database.GetTable(SystemeConstants.UserProfilesTableName)));
                if (!database.ExistTable(SystemeConstants.PrivilegesTableName)) database.AddTable(DefaultDataConstructor.CreatePrivilegesTable());
                if (!database.ExistTable(SystemeConstants.PrivilegesOfProfilesOnTablesTableName)) database.AddTable(DefaultDataConstructor.CreatePrivilegesOfProfilesTable(database.GetTable(SystemeConstants.ProfilesTableName), database.GetTable(SystemeConstants.PrivilegesTableName)));
            }
        }

        private void CreateDefaultDatabase(ISystemeDatabaseModule databaseModule)
        {
            if (!databaseModule.ExistDatabase(SystemeConstants.DefaultDatabaseName)) databaseModule.AddDatabase(new Database(SystemeConstants.DefaultDatabaseName));
        }

        public bool IsAcoplated()
        {
            return this.isAcoplated;
        }
    }
}
