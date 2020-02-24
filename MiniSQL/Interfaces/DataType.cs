using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class DataType
    {

        private ISaveDataFormatManager saveDataFormatManager;

        public abstract bool IsAValidDataType(string value);
        public abstract string GetSimpleTextValue();

        public void SetDataFormatManager(ISaveDataFormatManager format) 
        {
            this.saveDataFormatManager = format;
        }

        public string ParseLoadData(string data) 
        {
            return this.saveDataFormatManager.ParseFromLoad(data);
        }

        public string ParseToSaveData(string data) 
        {
            return this.saveDataFormatManager.ParseFromLoad(data);        
        }










    }
}
