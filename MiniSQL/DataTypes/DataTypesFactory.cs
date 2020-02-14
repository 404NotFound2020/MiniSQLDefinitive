using MiniSQL.Clases;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.DataTypes
{
    class DataTypesFactory
    {
        private static DataTypesFactory dataTypesFactory;
        private Dictionary<string, IDataType> dataTypes;

        private DataTypesFactory() 
        {
            this.dataTypes = new Dictionary<string, IDataType>();
            this.dataTypes.Add("IntType", IntType.GetIntType());
            this.dataTypes.Add("StringType", StringType.GetStringType());
            this.dataTypes.Add("DoubleType", DoubleType.GetDoubleType());
        }

        public static DataTypesFactory GetDataTypesFactory() 
        { 
            if(dataTypesFactory == null) {
                dataTypesFactory = new DataTypesFactory();
            }
            return dataTypesFactory;
        }

        public IDataType GetDataType(string dataTypeKey) 
        {
            return this.dataTypes[dataTypeKey];
        }











    }
}
