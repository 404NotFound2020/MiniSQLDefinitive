using MiniSQL.Classes;
using MiniSQL.ConfigurationClasses;
using MiniSQL.SystemeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ISysteme 
    {
        DatabaseProxy CreateDatabaseProxy(IDatabase database);
        ISystemeModule GetSystemeModule(string systemeModuleName);
        void SetSystemeModule(ISystemeModule systemeModule);
        void SetActiveModule(IActiveSystemModule activeModule);
        SystemConfiguration GetConfiguration();
        void SetupSysteme();
    }
}
