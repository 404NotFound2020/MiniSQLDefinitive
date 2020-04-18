using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestCreate
    {
        [TestMethod]
        public void CreateTable_GoodArguments_TableDoenstExist_CreateNewTable()
        {
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("testDatabase");            
            container.AddDatabase(database);
            string tableName = "testTable";
            Assert.IsFalse(database.ExistTable(tableName));
            Create create = CreateCreate(container, database.databaseName, tableName);
            List<Tuple<string, string>> columnsAndTypes = new List<Tuple<string, string>>() {new Tuple<string, string>("c1", TypesKeyConstants.StringTypeKey), new Tuple<string, string>("c2", TypesKeyConstants.IntTypeKey), new Tuple<string, string>("c3", TypesKeyConstants.DoubleTypeKey)};
            foreach(Tuple<string, string> tuple in columnsAndTypes) 
            {
                create.AddColumn(tuple.Item1, tuple.Item2);
            }
            Assert.IsTrue(create.ValidateParameters());
            create.Execute();
            Assert.IsTrue(database.ExistTable(tableName));
            ITable table = database.GetTable(tableName);
            bool allColumnExist = true;
            bool correctDataType = true;
            for(int i = 0; i < columnsAndTypes.Count && allColumnExist; i++) 
            {
                allColumnExist = table.ExistColumn(columnsAndTypes[i].Item1);
                if (allColumnExist) correctDataType = table.GetColumn(columnsAndTypes[i].Item1).dataType.GetSimpleTextValue().Equals(columnsAndTypes[i].Item2); 
            }
            Assert.IsTrue(allColumnExist);
            Assert.IsTrue(correctDataType);
            Console.WriteLine(create.GetResult());
        }

        [TestMethod]
        public void CreateTable_GoodArguments_TableExists_DontCreateNewTableAndNoticedinValidateParameters()
        {
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("testDatabase");            
            ITable table = new Table("table1");
            database.AddTable(table);
            container.AddDatabase(database);
            Assert.IsTrue(database.ExistTable(table.tableName)); //( ͡° ͜ʖ ͡°)
            Create create = CreateCreate(container, database.databaseName, table.tableName);
            create.AddColumn("rellenuto", TypesKeyConstants.StringTypeKey);
            Assert.IsFalse(create.ValidateParameters());
            create.Execute();
            Console.WriteLine(create.GetResult());
        }

        [TestMethod]
        public void CreateTable_BadArgumentsTwoColumnsWithSameName_DontCreateNewTableAndNotifiedInResult()
        {
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("testDatabase");
            container.AddDatabase(database);
            string tableName = "testTable";
            Assert.IsFalse(database.ExistTable(tableName));
            Create create = CreateCreate(container, database.databaseName, tableName);
            Tuple<string, string> columnAndType = new Tuple<string, string>("c1", TypesKeyConstants.StringTypeKey);
            create.AddColumn(columnAndType.Item1, columnAndType.Item2);
            create.AddColumn(columnAndType.Item1, columnAndType.Item2);
            Assert.IsFalse(create.ValidateParameters());
            create.Execute();
            Console.WriteLine(create.GetResult());
        }

        public static Create CreateCreate(IDatabaseContainer dataContainer, string databaseName, string tableName) 
        {
            Create create = new Create(dataContainer);
            create.targetDatabase = databaseName;
            create.targetTableName = tableName;
            return create;
        }
        
    }
}
