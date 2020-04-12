using MiniSQL.ConfigurationClasses;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SystemeClasses
{
    public class SystemeDatabaseContainerModule : ISystemeDatabaseModule
    {
        private AbstractParser parser;
        private DatabaseBin databaseContainer;
        private ISysteme systeme;
        private static SystemeDatabaseContainerModule systemeDatabaseContainerModule;
        private bool isAcoplated;

        private SystemeDatabaseContainerModule()
        {
            this.databaseContainer = new DatabaseBin(this);
            this.isAcoplated = false;
        }

        public static SystemeDatabaseContainerModule GetSystemeDatabaseContainerModule()
        {
            if (systemeDatabaseContainerModule == null) systemeDatabaseContainerModule = new SystemeDatabaseContainerModule();
            return systemeDatabaseContainerModule;
        }

        public string GetDefaultDatabaseName()
        {
            return SystemeConstants.DefaultDatabaseName;
        }

        public void SetSysteme(ISysteme system)
        {
            this.systeme = system;
            SystemConfiguration configuration = this.systeme.GetConfiguration();
            ParserBuilder builder = ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(configuration.parserVersion);
            builder.SetUbicationManager(configuration.ubicationVersion);
            builder.SetDataFormatManager(configuration.saveDataVersion);
            this.parser = builder.GetParser();
        }

        public void ActToAdd(IDatabase database)
        {
            this.parser.SaveDatabase(database);
        }

        public void ActToAdd(IDatabase database, ITable table)
        {
            this.parser.SaveTable(database, table);
        }

        public void ActToRemove(IDatabase database)
        {
            this.parser.DeleteDatabase(database.databaseName);
        }

        public void ActToRemove(IDatabase database, ITable table)
        {
            this.parser.DeleteTable(database.databaseName, table.tableName);
        }

        public string GetModuleKey()
        {
            return SystemeConstants.SystemeDatabaseModule;
        }

        public void TableModified(IDatabase database, ITable table)
        {
            this.parser.SaveTable(database, table);
        }

        public void SaveAll()
        {
            IEnumerator<IDatabase> databaseEnumerator = this.databaseContainer.activeDatabases.Values.GetEnumerator();
            while (databaseEnumerator.MoveNext()) this.parser.SaveDatabase(databaseEnumerator.Current);
        }

        public void AcoplateTheModule()
        {
            if (!this.isAcoplated)
            {
                string[] databasesNames = this.parser.GetDatabasesNames();
                IDatabase database;
                for (int i = 0; i < databasesNames.Length; i++)
                {
                    database = this.parser.LoadDatabase(databasesNames[i]);
                    this.databaseContainer.activeDatabases.Add(database.databaseName, database);
                }
                this.isAcoplated = true;
            }
        }

        public bool IsAcoplated()
        {
            return this.isAcoplated;
        }

        public IDatabaseContainer GetDatabaseContainer()
        {
            return this.databaseContainer;
        }

        public ISysteme GetSysteme()
        {
            return this.systeme;
        }

        public IDatabase GetDatabase(string databaseName)
        {
            return this.databaseContainer.activeDatabases[databaseName];
        }

        public void AddDatabase(IDatabase database)
        {
            this.databaseContainer.activeDatabases.Add(database.databaseName, database);
        }
    }
}
