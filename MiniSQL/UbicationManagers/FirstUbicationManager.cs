using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.UbicationManagers
{
    public class FirstUbicationManager : IUbicationManager
    {
        private static FirstUbicationManager firstUbicationManager;
        private const string additionalPath = "data";

        private FirstUbicationManager() 
        {
            if(!Directory.Exists(additionalPath)) Directory.CreateDirectory(additionalPath);           
        }

        public static FirstUbicationManager GetFirstUbicationManager() 
        { 
            if(firstUbicationManager == null) 
            {
                firstUbicationManager = new FirstUbicationManager();
            }
            return firstUbicationManager;
        }


        public string GetDatabaseFilePath(string databaseName)
        {
            return additionalPath+databaseName;
        }

        public string GetTableDataFilePath(string tableName)
        {
            return tableName;
        }

        public string GetTableStructureFilePath(string tableName)
        {
            return tableName + "STR";
        }
    }
}
