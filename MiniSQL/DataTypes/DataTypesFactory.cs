using MiniSQL.Clases;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.DataTypes
{
    public class DataTypesFactory
    {
        private static DataTypesFactory dataTypesFactory;
        private Dictionary<string, DataType> dataTypes;

        private DataTypesFactory() 
        {
            this.dataTypes = new Dictionary<string, DataType>();
            this.dataTypes.Add(TypesKeyConstants.IntTypeKey, IntType.GetIntType());
            this.dataTypes.Add(TypesKeyConstants.StringTypeKey, StringType.GetStringType());
            this.dataTypes.Add(TypesKeyConstants.DoubleTypeKey, DoubleType.GetDoubleType());
        }

        public static DataTypesFactory GetDataTypesFactory() 
        { 
            if(dataTypesFactory == null) {
                dataTypesFactory = new DataTypesFactory();
            }
            return dataTypesFactory;
        }

        public DataType GetDataType(string dataTypeKey) 
        {
            return this.dataTypes[dataTypeKey];
        }











    }
}
