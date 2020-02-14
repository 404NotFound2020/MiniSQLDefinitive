using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    interface IDataType
    {
        bool IsAValidDataType(string value);

    }
}
