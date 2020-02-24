using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.SaveDataFormatManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Parsers
{
    public class DataFormatFactory
    {
        private static DataFormatFactory dataFormatFactory;

        private DataFormatFactory() 
        { 
        
        }

        public static DataFormatFactory GetDataFormatFactory() {
            if(dataFormatFactory == null) 
            {
                dataFormatFactory = new DataFormatFactory();
            }
            return dataFormatFactory;
        }

        public ISaveDataFormatManager GetSaveDataFormatManager(string dataFormatVersion) {
            ISaveDataFormatManager saveDataFormatManager = null;
            switch (dataFormatVersion) 
            {
                case SaveDataFormatVersions.NothingToDoDataFormatManagerVersion:
                    saveDataFormatManager = NothingToDoManager.GetNothingToDoManager();
                    break;
                case SaveDataFormatVersions.StringEscapedFormatManagerVersion:
                    saveDataFormatManager = StringEscapedManager.GetStringEscapedManager();
                    break;   
            }
            return saveDataFormatManager;
        }






    }
}
