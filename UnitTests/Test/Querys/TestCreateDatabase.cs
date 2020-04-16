using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestCreateDatabase
    {
        [TestMethod]
        public void CreateDatabase_DatabaseExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string databaseName = VariousFunctions.GenerateRandomString(8);
            while (databaseContainer.ExistDatabase(databaseName))
            {
                databaseName = VariousFunctions.GenerateRandomString(8);
            }
            IDatabase database = new Database(databaseName);
            databaseContainer.AddDatabase(database);
            CreateDatabase createDatabase = CreateCreateDatabase(databaseContainer, database.databaseName);
            Assert.IsFalse(createDatabase.ValidateParameters());
        }

        [TestMethod]
        public void CreateDatabase_DatabaseDoenstExist_CreateDatabase()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string databaseName = VariousFunctions.GenerateRandomString(8);
            while (databaseContainer.ExistDatabase(databaseName))
            {
                databaseName = VariousFunctions.GenerateRandomString(8);
            }
            CreateDatabase createDatabase = CreateCreateDatabase(databaseContainer, databaseName);
            Assert.IsTrue(createDatabase.ValidateParameters());
            createDatabase.Execute();
            Assert.IsTrue(databaseContainer.ExistDatabase(databaseName));
        }

        public static CreateDatabase CreateCreateDatabase(IDatabaseContainer databaseContainer, string databaseName)
        {
            CreateDatabase createDatabase = new CreateDatabase(databaseContainer);
            createDatabase.targetDatabase = databaseName;
            return createDatabase;
        }

    }
}
