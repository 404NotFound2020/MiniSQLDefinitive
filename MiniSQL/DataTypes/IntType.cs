using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.DataTypes
{
    public class IntType : DataType
    {

        private static IntType intType;

        private IntType() 
        { 
        
        }

        public static IntType GetIntType() 
        {
            if (intType == null) {
                intType = new IntType();
            }
            return intType;        
        }

        public override bool Evaluate(Operator opera, string data1, string data2)
        {
            return opera.evaluate(int.Parse(data1), int.Parse(data2));
        }

        public override string GetSimpleTextValue()
        {
            return TypesKeyConstants.IntTypeKey;
        }

        public override bool IsAValidDataType(string value)
        {
            return int.TryParse(value, out int waste);
        }

        public override string GetDataTypeDefaultValue()
        {
            return "0";
        }


    }
}
