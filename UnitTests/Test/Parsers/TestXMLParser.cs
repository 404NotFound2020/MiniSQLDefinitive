using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using UnitTests.Test.TestObjectsContructor;
using MiniSQL.Parsers;
using MiniSQL.Interfaces;
using MiniSQL.Constants;
using MiniSQL.Comparers;

namespace UnitTests.Test.Parsers
{
    [TestClass]
    public class TestXMLParser
    {
        [TestMethod]
        public void SaveAndLoadDatabase()
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            Database loadedDatabase = xmlParser.LoadDatabase(testDatabase.databaseName);
            Assert.IsTrue(new DatabaseComparer().Equals(testDatabase, loadedDatabase));
        }

        [TestMethod]
        public void DeleteDatabase_DatabaseExist_DoTheThingsOK()
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            Assert.IsTrue(xmlParser.ExistDatabase(testDatabase.databaseName));
            xmlParser.DeleteDatabase(testDatabase.databaseName);
            Assert.IsFalse(xmlParser.ExistDatabase(testDatabase.databaseName));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "")]    
        public void DeleteDatabase_DatabaseDoesntExist_ThrowException()
        {
            AbstractParser xmlParser = CreateXMLParser();
            string randomDatabaseName = VariousFunctions.GenerateRandomString(6);
            while (xmlParser.ExistDatabase(randomDatabaseName))
            {
                randomDatabaseName = VariousFunctions.GenerateRandomString(10);
            }
            xmlParser.LoadDatabase(randomDatabaseName);
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
