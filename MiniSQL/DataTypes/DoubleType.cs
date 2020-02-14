using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.DataTypes
{
    public class DoubleType : IDataType
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

        public bool IsAValidDataType(string value)
        {
            throw new NotImplementedException();
        }
    }
}
