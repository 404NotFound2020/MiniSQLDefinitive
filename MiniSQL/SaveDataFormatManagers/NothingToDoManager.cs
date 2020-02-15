using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SaveDataFormatManagers
{
    public class NothingToDoManager : ISaveDataFormatManager
    {

        private static NothingToDoManager nothingToDoManager;

        private NothingToDoManager() 
        { 
        
        }

        public static NothingToDoManager GetNothingToDoManager() 
        {
            if (nothingToDoManager == null) nothingToDoManager = new NothingToDoManager();
            return nothingToDoManager;
        }

        public string ParseFromLoad(string data)
        {
            return data;
        }

        public string ParseToSave(string data)
        {
            return data;
        }
    }
}
