using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.UbicationManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Parsers
{
    public class UbicationManagerFactory
    {

        private static UbicationManagerFactory ubicationManagerFactory;

        private UbicationManagerFactory() 
        { 
        
        }

        public static UbicationManagerFactory GetUbicationManagerFactory() 
        { 
            if(ubicationManagerFactory == null) 
            {
                ubicationManagerFactory = new UbicationManagerFactory();
            }
            return ubicationManagerFactory;        
        }

        public IUbicationManager GetUbicationManager(string ubicationVersion) 
        {
            IUbicationManager ubicationManager = null;
            switch (ubicationVersion) 
            {
                case UbicationVersions.FirstUbicationVersion:
                    ubicationManager = FirstUbicationManager.GetFirstUbicationManager();
                    break;            
            }
            return ubicationManager;
        }


    }
}
