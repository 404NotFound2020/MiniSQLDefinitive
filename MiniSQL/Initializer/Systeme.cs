using MiniSQL.Classes;
using MiniSQL.ConfigurationClasses;
using MiniSQL.Constants;
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
            this.ChargeTheSystem();
            this.ChargeTheDatabases();
            this.CreateDefaultDatabase();
            this.CreateSystemDatabases();
        }

        private void ChargeTheSystem() 
        {
            this.configuration = ConfigurationParser.GetConfigurationParser().GetSystemConfiguration();
            ParserBuilder builder = ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(this.configuration.parserVersion);
            builder.SetUbicationManager(this.configuration.ubicationVersion);
            builder.SetDataFormatManager(this.configuration.saveDataVersion);
            builder.SetIndexationVersion(this.configuration.indexationVersion);
            this.parser = builder.GetParser();
        }

        public void ChargeTheDatabases() 
        {
            this.activeDatabases = new Dictionary<string, Database>();
            string[] databasesNames = this.parser.GetDatabasesNames();           
            Database database;
            for(int i = 0; i < databasesNames.Length; i++) 
            {
                database = this.parser.LoadDatabase(databasesNames[i]);
                this.activeDatabases.Add(database.databaseName, database);
            }        
        }

        public void CreateDefaultDatabase() 
        {
            if (!this.activeDatabases.ContainsKey(SystemeConstants.DefaultDatabaseName)) 
            { 
                Database defaultDatabase = new Database(SystemeConstants.DefaultDatabaseName);
                this.activeDatabases.Add(defaultDatabase.databaseName, defaultDatabase);
                this.parser.SaveDatabase(defaultDatabase);
            }
        }

        private void CreateSystemDatabases() 
        {
            Database database;
            if (!this.activeDatabases.ContainsKey(SystemeConstants.SystemDatabaseName)) {
                database = DefaultDataConstructor.CreateSystemDatabase();
                this.activeDatabases.Add(database.databaseName, database);
                this.parser.SaveDatabase(database);
            }
            else 
            {
                database = this.activeDatabases[SystemeConstants.SystemDatabaseName];
                if (!database.ExistTable(SystemeConstants.UsersTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreateUsersTable());
                if (!database.ExistTable(SystemeConstants.ProfilesTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreateProfilesTable());
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
            this.parser.SaveDatabase(database);
        }

        public void RemoveDatabase(string databaseName)
        {
            activeDatabases.Remove(databaseName);
            this.parser.DeleteDatabase(databaseName);
        }

        public int GetNumbersOfDatabases()
        {
            return this.activeDatabases.Count;
        }

        public void SaveTable(Database database, Table table)
        {
            this.parser.SaveTable(database, table);
        }

        public void RemoveTable(Database database, Table table)
        {
            this.parser.DeleteTable(database.databaseName, table.tableName);
        }

        public void SaveData()
        {
            IEnumerator<Database> enumerator = this.activeDatabases.Values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                this.SaveData(enumerator.Current);
            }
        }

        public void SaveData(Database database)
        {
            this.parser.SaveDatabase(database);
        }

        public string GetDefaultDatabaseName()
        {
            return SystemeConstants.DefaultDatabaseName;
        }

        private void AddNewTableToADatabase(Database database, Table table) 
        {
            database.AddTable(table);
            parser.SaveTable(database, table);
        }
    }
}
