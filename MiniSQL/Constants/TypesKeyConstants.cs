using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Constants
{
    public class TypesKeyConstants
    {
        public const string StringTypeKey = "TEXT";
        public const string IntTypeKey = "INT";
        public const string DoubleTypeKey = "DOUBLE";

        public static List<string> ReturnKeys() {
            List<string> keys = new List<string>();
            keys.Add(StringTypeKey);
            keys.Add(IntTypeKey);
            keys.Add(DoubleTypeKey);
            return keys;
        }


    }
}
