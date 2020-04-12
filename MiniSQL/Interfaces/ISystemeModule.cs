using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ISystemeModule
    {
        ISysteme GetSysteme();
        void SetSysteme(ISysteme system);
        string GetModuleKey();        
        void AcoplateTheModule();
        bool IsAcoplated();
    }
}
