using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestInsert
    {
        [TestMethod]
        public void Insert_GoodArguments_TheRowHasInserted()
        {
        
        }

        [TestMethod]
        public void Insert_BadArguments_ConcretelyTableDoesntExist_NotifiedInResult()
        {
            Database database = ObjectConstructor.CreateDatabaseFull("test1");
            Table table = database.GetTable("Table1");
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            databaseContainer.AddDatabase(database);
            Insert insert = insertedDate(databaseContainer);
            insert.targetDatabase = database.databaseName;
            insert.targetTableName = table.tableName;
            insert.ValidateParameters();
            insert.Execute();
            Console.WriteLine(insert.GetResult());
        }


        private Insert insertedDate(IDatabaseContainer databaseContainer)
        {
            return new Insert(databaseContainer);
        }
    }
}
