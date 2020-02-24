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
        private static StringEscapedManager stringEscapedManager;

        private StringEscapedManager()
        {

        }

        public static StringEscapedManager GetStringEscapedManager() 
        { 
            if(stringEscapedManager == null) 
            {
                stringEscapedManager = new StringEscapedManager();
            }
            return stringEscapedManager;
        }

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
