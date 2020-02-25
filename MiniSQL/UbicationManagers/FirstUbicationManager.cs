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
        private const string additionalPath = "BD";

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
            return additionalPath + "\\" + databaseName;
        }

        public string GetTableDataFilePath(string databaseName, string tableName)
        {
            return this.GetDatabaseFilePath(databaseName) + "\\" + tableName;
        }

        public string GetTableStructureFilePath(string databaseName, string tableName)
        {
            return this.GetDatabaseFilePath(databaseName) + "\\" + tableName + "STR";
        }

        public string GetDatabasePropertiesFilePath(string databaseName)
        {
            return this.GetDatabaseFilePath(databaseName) + "\\" + databaseName;
        }
    }
}
