using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using UnitTests.Test.TestObjectsContructor;
using MiniSQL.Parsers;
using MiniSQL.Interfaces;
using MiniSQL.Constants;
using MiniSQL.Comparers;
using System.Collections.Generic;

namespace UnitTests.Test.Parsers
{
    [TestClass]
    public class TestXMLParser
    {
        [TestMethod]
        [DoNotParallelize]
        public void SaveAndLoadDatabase()
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            Database loadedDatabase = xmlParser.LoadDatabase(testDatabase.databaseName);           
            Assert.IsTrue(Database.GetDatabaseComparer().Equals(testDatabase, loadedDatabase));
        }

        [TestMethod]
        [DoNotParallelize]
        public void ExistTable_TableExist_ReturnTrue() 
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            IEnumerator<Table> enumerator = testDatabase.GetTableEnumerator();
            if (enumerator.MoveNext()) {
                Assert.IsTrue(xmlParser.ExistTable(testDatabase.databaseName, enumerator.Current.tableName));
            }
            else
            {
                Assert.Fail("there is no tables idiot!");
            }            
        }

        [TestMethod]
        [DoNotParallelize]
        public void ExistTable_TableNoExist_ReturnFalse()
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            string randomTableName = VariousFunctions.GenerateRandomString(6);
            while (testDatabase.ExistTable(randomTableName)) 
            { 
                randomTableName = VariousFunctions.GenerateRandomString(6);
            }
            Assert.IsFalse(xmlParser.ExistTable(testDatabase.databaseName, randomTableName));

        }

        [TestMethod]
        [DoNotParallelize]
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
        [DoNotParallelize]
        [ExpectedException(typeof(Exception))]    
        public void DeleteDatabase_DatabaseDoesntExist_ThrowException()
        {
            AbstractParser xmlParser = CreateXMLParser();
            string randomDatabaseName = VariousFunctions.GenerateRandomString(6);
            while (xmlParser.ExistDatabase(randomDatabaseName))
            {
                randomDatabaseName = VariousFunctions.GenerateRandomString(10);
            }
            Assert.IsFalse(xmlParser.ExistDatabase(randomDatabaseName));
            xmlParser.DeleteDatabase(randomDatabaseName);
        }

        [TestMethod]
        [DoNotParallelize]
        public void DeleteTable_TableExist_DeleteThen()
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            IEnumerator<Table> enumerator = testDatabase.GetTableEnumerator();
            if (enumerator.MoveNext())
            {
                Assert.IsTrue(xmlParser.ExistTable(testDatabase.databaseName, enumerator.Current.tableName));
                xmlParser.DeleteTable(testDatabase.databaseName, enumerator.Current.tableName);
                Assert.IsFalse(xmlParser.ExistTable(testDatabase.databaseName, enumerator.Current.tableName));
            }
            else
            {
                Assert.Fail("there is no tables idiot!");
            }
        }

        [TestMethod]
        [DoNotParallelize]
        [ExpectedException(typeof(Exception))]    
        public void DeleteTable_TableNoExist_ThrowException()
        {
            AbstractParser xmlParser = CreateXMLParser();
            Database testDatabase = ObjectConstructor.CreateDatabaseFull();
            xmlParser.SaveDatabase(testDatabase);
            string randomTableName = VariousFunctions.GenerateRandomString(6);
            while (testDatabase.ExistTable(randomTableName))
            {
                randomTableName = VariousFunctions.GenerateRandomString(6);
            }
            Assert.IsFalse(xmlParser.ExistTable(testDatabase.databaseName, randomTableName));
            xmlParser.DeleteTable(testDatabase.databaseName, randomTableName);
        }






        public static AbstractParser CreateXMLParser() 
        {
            ParserBuilder builder = ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(ParserVersions.XMLParserVersion);
            builder.SetUbicationManager(LastVersionVariables.UbicationVersion);
           // builder.SetIndexationVersion(LastVersionVariables.ActualIndexationVersion);
            builder.SetDataFormatManager(LastVersionVariables.SaveDataFormatVersion);
            return builder.GetParser();       
        }

    }
}
