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
            IDatabaseContainer databaseContainer = databaseModule.GetDatabaseContainer();
            if (!databaseContainer.ExistDatabase(SystemeConstants.SystemDatabaseName)) databaseModule.AddDatabase(DefaultDataConstructor.CreateSystemDatabase());
            else DefaultDataConstructor.CompleteSystemDatabase(databaseModule.GetDatabase(SystemeConstants.SystemDatabaseName));
        }

        private void CreateDefaultDatabase(ISystemeDatabaseModule databaseModule)
        {
            IDatabaseContainer databaseContainer = databaseModule.GetDatabaseContainer();
            if (!databaseContainer.ExistDatabase(SystemeConstants.DefaultDatabaseName)) databaseContainer.AddDatabase(new Database(SystemeConstants.DefaultDatabaseName));
        }

        public bool IsAcoplated()
        {
            return this.isAcoplated;
        }

        public ISysteme GetSysteme()
        {
            return this.systeme;
        }

    }
}
