using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Clases
{
    public class StringType : DataType
    {

        private static StringType stringType;

        private StringType() 
        { 
        
        }

        public static StringType GetStringType() 
        {
            if (stringType == null) {
                stringType = new StringType();
            }
            return stringType;        
        }

        public override bool Evaluate(Operator opera, string data1, string data2)
        {
            return opera.evaluate(data1, data2);
        }

        public override string GetSimpleTextValue()
        {
            return TypesKeyConstants.StringTypeKey;
        }

        public override bool IsAValidDataType(string value)
        {
            return true;
        }
        public override string GetDataTypeDefaultValue()
        {
            return "null";
        }
    }
}
