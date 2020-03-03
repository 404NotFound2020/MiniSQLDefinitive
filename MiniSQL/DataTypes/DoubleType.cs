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
    public class DoubleType : DataType
    {

        private static DoubleType doubleType;

        private DoubleType() 
        { 
        
        }

        public static DoubleType GetDoubleType() 
        {
            if (doubleType == null) {
                doubleType = new DoubleType();
            }
            return doubleType;       
        }

        public override bool Evaluate(Operator opera, string data1, string data2)
        {
            return opera.evaluate(double.Parse(data1), double.Parse(data2));
        }

        public override string GetSimpleTextValue()
        {
            return TypesKeyConstants.DoubleTypeKey;
        }

        public override bool IsAValidDataType(string value)
        {
            return double.TryParse(value, out double waste);
        }
    }
}
