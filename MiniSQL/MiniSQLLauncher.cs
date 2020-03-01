using MiniSQL.Initializer;
using MiniSQL.UbicationManagers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL
{
    public class MiniSQLLauncher
    {
        public static void Start()
        {
            ConfigurationParser.GetConfigurationParser();

        }

    }
}
