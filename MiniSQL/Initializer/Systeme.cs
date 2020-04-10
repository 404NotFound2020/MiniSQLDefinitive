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

        private Dictionary<string, IDatabase> activeDatabases;        
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
            this.parser = builder.GetParser();
        }

        public void ChargeTheDatabases() 
        {
            this.activeDatabases = new Dictionary<string, IDatabase>();
            string[] databasesNames = this.parser.GetDatabasesNames();           
            IDatabase database;
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
            IDatabase database;
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
                if (!database.ExistTable(SystemeConstants.NoRemovableUsersTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreateNoRemovableUsersTable(database.GetTable(SystemeConstants.UsersTableName)));
                if (!database.ExistTable(SystemeConstants.NoRemovableProfilesTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreateNoRemovableProfilesTable(database.GetTable(SystemeConstants.ProfilesTableName)));
                if (!database.ExistTable(SystemeConstants.UserProfilesTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreateUserProfilesTable(database.GetTable(SystemeConstants.UsersTableName), database.GetTable(SystemeConstants.ProfilesTableName)));
                if (!database.ExistTable(SystemeConstants.NoRemovableUserProfilesTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreateNoRemovableUserProfilesTable(database.GetTable(SystemeConstants.UserProfilesTableName)));
                if (!database.ExistTable(SystemeConstants.PrivilegesTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreatePrivilegesTable());
                if (!database.ExistTable(SystemeConstants.PrivilegesOfProfilesOnTablesTableName)) this.AddNewTableToADatabase(database, DefaultDataConstructor.CreatePrivilegesOfProfilesTable(database.GetTable(SystemeConstants.ProfilesTableName), database.GetTable(SystemeConstants.PrivilegesTableName)));
            }
        }

        public static Systeme GetSystem() 
        {
            return system;
        }

        public IDatabase GetDatabase(string databaseName)
        {
            return activeDatabases[databaseName];
        }

        public bool ExistDatabase(string databaseName)
        {
            return activeDatabases.ContainsKey(databaseName);
        }

        public void AddDatabase(IDatabase database)
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

        public void SaveTable(IDatabase database, ITable table)
        {
            this.parser.SaveTable(database, table);
        }

        public void RemoveTable(IDatabase database, ITable table)
        {
            this.parser.DeleteTable(database.databaseName, table.tableName);
        }

        public void SaveData()
        {
            IEnumerator<IDatabase> enumerator = this.activeDatabases.Values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                this.SaveData(enumerator.Current);
            }
        }

        public void SaveData(IDatabase database)
        {
            this.parser.SaveDatabase(database);
        }

        public string GetDefaultDatabaseName()
        {
            return SystemeConstants.DefaultDatabaseName;
        }

        private void AddNewTableToADatabase(IDatabase database, ITable table) 
        {
            database.AddTable(table);
            parser.SaveTable(database, table);
        }
    }
}
