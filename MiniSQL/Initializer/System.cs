using MiniSQL.Classes;
using MiniSQL.ConfigurationClasses;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public class System
    {

        private Dictionary<string, Database> activeDatabases;        
        private SystemConfiguration configuration;
        private static System system = new System();

        private System() 
        {
            
        }

        public static System GetSystem() 
        {
            return system;
        }

 
    }
}
