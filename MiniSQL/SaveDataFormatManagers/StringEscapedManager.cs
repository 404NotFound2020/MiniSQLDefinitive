using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SaveDataFormatManagers
{
    class StringEscapedManager : ISaveDataFormatManager
    {
        public string ParseFromLoad(string data)
        {
            return data.Substring(1, data.Length - 2);
        }

        public string ParseToSave(string data)
        {
            return '"' + data + '"';
        }
    }
}
