using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using UnitTests.Test.TestObjectsContructor;
using MiniSQL.Parsers;
using MiniSQL.Interfaces;
using MiniSQL.Constants;

namespace UnitTests.Test.Parsers
{
    [TestClass]
    public class TestXMLParser
    {
        [TestMethod]
        public void SaveDatabase()
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            Database loadedDatabase = xmlParser.LoadDatabase(testDatabase.databaseName);
            Assert.IsTrue(CompareDatabase(testDatabase, loadedDatabase));
        }

        [TestMethod]
        public void LoadDatabase()
        {

        }

        [TestMethod]
        public void DeleteDatabase()
        {

        }

        [TestMethod]
        public void DeleteTable()
        {

        }

        [TestMethod]
        public void LoadTable()
        {

        }

        [TestMethod]
        public void SaveTable()
        {

        }

        public static bool CompareDatabase(Database database1, Database database2) 
        {
            bool b = false;


            return b;
        }

        public static AbstractParser CreateXMLParser() 
        {
            ParserBuilder builder = ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(ParserVersions.XMLParserVersion);
            builder.SetUbicationManager(LastVersionVariables.UbicationVersion);
            builder.SetIndexationVersion(LastVersionVariables.ActualIndexationVersion);
            builder.SetDataFormatManager(LastVersionVariables.SaveDataFormatVersion);
            return builder.GetParser();       
        }

    }
}
