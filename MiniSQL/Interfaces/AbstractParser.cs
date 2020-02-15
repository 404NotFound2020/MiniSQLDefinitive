using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class AbstractParser
    {
        private IUbicationManager ubicationManager;

        public abstract Database LoadDatabase(string databaseName);
        public abstract bool SaveDatabase(string databaseName);

        public void SetUbicationManager(IUbicationManager ubicationManager) 
        { 
        
        
        }

    }
}
