using MiniSQL.Classes;
using MiniSQL.ConfigurationClasses;
using MiniSQL.Constants;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using MiniSQL.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SystemeClasses
{
    public class Systeme : ISysteme
    {

        private Dictionary<string, ISystemeModule> systemeModules;
        private List<IActiveSystemModule> activeSystemeModulesList;
     
        private SystemConfiguration configuration;

        private static Systeme system = new Systeme();

        private Systeme() 
        {
            this.systemeModules = new Dictionary<string, ISystemeModule>();
            this.activeSystemeModulesList = new List<IActiveSystemModule>();
            this.configuration = ConfigurationParser.GetConfigurationParser().GetSystemConfiguration();            
        }

        public static Systeme GetSystem() 
        {
            return system;
        }

        public ISystemeModule GetSystemeModule(string systemeModuleName)
        {
            return this.systemeModules[systemeModuleName];
        }

        public void SetSystemeModule(ISystemeModule systemeModule)
        {
            this.systemeModules.Add(systemeModule.GetModuleKey(), systemeModule);
        }

        public void SetActiveModule(IActiveSystemModule activeModule)
        {
            this.SetSystemeModule(activeModule);
            this.activeSystemeModulesList.Add(activeModule);
        }

        public SystemConfiguration GetConfiguration()
        {
            return this.configuration;
        }
   
        public List<IActiveSystemModule> GetActiveModuleList()
        {
            return this.activeSystemeModulesList;
        }
    }
}
