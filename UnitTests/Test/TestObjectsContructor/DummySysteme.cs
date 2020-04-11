using MiniSQL.ConfigurationClasses;
using MiniSQL.Interfaces;
using MiniSQL.SystemeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    public class DummySysteme : ISysteme
    {
        private Dictionary<string, ISystemeModule> systemeModules;
        private List<IActiveSystemModule> activeSystemeModulesList;

        public DummySysteme()
        {
            this.systemeModules = new Dictionary<string, ISystemeModule>();
            this.activeSystemeModulesList = new List<IActiveSystemModule>();
        }

        public DatabaseProxy CreateDatabaseProxy(IDatabase database)
        {
            return null;
        }

        public SystemConfiguration GetConfiguration()
        {
            return null;
        }

        public ISystemeModule GetSystemeModule(string systemeModuleName)
        {
            return this.systemeModules[systemeModuleName];
        }

        public void SetActiveModule(IActiveSystemModule activeModule)
        {
            this.SetSystemeModule(activeModule);
            this.activeSystemeModulesList.Add(activeModule);
        }

        public void SetSystemeModule(ISystemeModule systemeModule)
        {
            this.systemeModules.Add(systemeModule.GetModuleKey(), systemeModule);
        }

        public void SetupSysteme()
        {
            
        }
    }
}
