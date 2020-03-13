using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.ConfigurationClasses;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using MiniSQL.Parsers;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Initializer
{
    [TestClass]
    public class TestSystem
    {
        [TestMethod]
        public void TestIfTheDatabasesAreLoad()
        {            
            Systeme system = Systeme.GetSystem();
            SystemConfiguration configuration = ConfigurationParser.GetConfigurationParser().GetSystemConfiguration();
            AbstractParser parser = GetTheParser(configuration);           
            bool okLoad = true;
            string[] databasesNames = parser.GetDatabasesNames();
            for(int i = 0; i < databasesNames.Length && okLoad; i++) 
            {
                okLoad = system.ExistDatabase(databasesNames[i]);
                if (okLoad) okLoad = Database.GetDatabaseComparer().Equals(system.GetDatabase(databasesNames[i]), parser.LoadDatabase(databasesNames[i]));
            }
            Assert.IsTrue(okLoad);
        }

        public static AbstractParser GetTheParser(SystemConfiguration configuration) {
            ParserBuilder builder = ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(configuration.parserVersion);
            builder.SetUbicationManager(configuration.ubicationVersion);
            builder.SetDataFormatManager(configuration.saveDataVersion);
            AbstractParser parser = builder.GetParser();
            return parser;
        }







    }
}
