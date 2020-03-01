using MiniSQL.ConfigurationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public class ConfigDelimitator
    {
        public string delimitatorName;
        public string defaultValue;
        public Action<Tuple<SystemConfiguration, string>> setValueToConfig;

        public ConfigDelimitator(string delimitatorName, string defaultValue, Action<Tuple<SystemConfiguration, string>> setFunction) 
        {
            this.delimitatorName = delimitatorName;
            this.defaultValue = defaultValue;
            this.setValueToConfig = setFunction;
        }

    }
}
