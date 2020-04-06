using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestDropDatabase
    {
        [TestMethod]
        public void DropDatabase_DatabaseExist_DropDatabase()
        {
            //Construct phase
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("database");
            databaseContainer.AddDatabase(database);
            DropDatabase dropDatabase = CreateDropDatabase(databaseContainer, database.databaseName);
            //Test phase
            Assert.IsTrue(dropDatabase.ValidateParameters());
            dropDatabase.Execute();
            Assert.IsFalse(databaseContainer.ExistDatabase(database.databaseName));
        }

        [TestMethod]
        public void DropDatabase_DatabaseDoesntExist_NoticeInValidate()
        {
            //Construct phase
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string databaseName = "tast1";
            DropDatabase dropDatabase = CreateDropDatabase(databaseContainer, databaseName);
            //Test phase
            Assert.IsFalse(databaseContainer.ExistDatabase(databaseName));
            Assert.IsFalse(dropDatabase.ValidateParameters());
        }

        public static DropDatabase CreateDropDatabase(IDatabaseContainer databaseContainer, string databaseName)
        {
            DropDatabase dropDatabase = new DropDatabase(databaseContainer);
            dropDatabase.targetDatabase = databaseName;
            return dropDatabase;
        }

    }
}