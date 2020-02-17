using MiniSQL.Constants;
using MiniSQL.Interfaces;
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
