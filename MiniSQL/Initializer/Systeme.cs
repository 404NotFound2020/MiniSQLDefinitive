﻿using MiniSQL.Classes;
using MiniSQL.ConfigurationClasses;
using MiniSQL.Interfaces;
using MiniSQL.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public class Systeme : IDatabaseContainer
    {

        private Dictionary<string, Database> activeDatabases;        
        private SystemConfiguration configuration;
        private AbstractParser parser;
        private static Systeme system = new Systeme();

        private Systeme() 
        {
            this.activeDatabases = new Dictionary<string, Database>();
            this.ChargeTheSystem();
        }

        private void ChargeTheSystem() 
        {
            this.configuration = ConfigurationParser.GetConfigurationParser().GetSystemConfiguration();
            ParserBuilder builder = ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(this.configuration.parserVersion);
            builder.SetUbicationManager(this.configuration.ubicationVersion);
            builder.SetDataFormatManager(this.configuration.saveDataVersion);
            this.parser = builder.GetParser();
        }

        public void ChargeTheDatabases() 
        {
            string[] databasesNames = this.parser.GetDatabasesNames();
            Database database;
            for(int i = 0; i < databasesNames.Length; i++) 
            {
                database = this.parser.LoadDatabase(databasesNames[i]);
                this.activeDatabases.Add(database.databaseName, database);
            }        
        }

        public static Systeme GetSystem() 
        {
            return system;
        }

        public Database GetDatabase(string databaseName)
        {
            return activeDatabases[databaseName];
        }

        public bool ExistDatabase(string databaseName)
        {
            return activeDatabases.ContainsKey(databaseName);
        }

        public void AddDatabase(Database database)
        {
            activeDatabases.Add(database.databaseName,database);
        }

        public void RemoveDatabase(string databaseName)
        {
            activeDatabases.Remove(databaseName);
        }

        public int GetNumbersOfDatabases()
        {
            return this.activeDatabases.Count;
        }

        public void SaveData()
        {
            IEnumerator<Database> enumerator = this.activeDatabases.Values.GetEnumerator();
            while (enumerator.MoveNext()) 
            {
                this.parser.SaveDatabase(enumerator.Current);
            }
        }
    }
}