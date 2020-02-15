using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ISaveDataFormatManager
    {
        string ParseFromLoad(string data);
        string ParseToSave(string data);


    }
}
