using MiniSQL.Interfaces;
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

        public override bool IsAValidDataType(string value)
        {
            throw new NotImplementedException();
        }
    }
}
