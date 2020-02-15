using MiniSQL.Interfaces;
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

        public override bool IsAValidDataType(string value)
        {
            throw new NotImplementedException();
        }
    }
}
