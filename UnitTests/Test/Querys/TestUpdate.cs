using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{

    [TestClass]
    public class TestUpdate
    {

        public Update CreateUpdate(IDatabaseContainer databaseContainer, string databaseName, string tableName)
        {
            Update update = new Update(databaseContainer);
            update.targetDatabase = databaseName;
            update.targetTableName = tableName;
            return update;
        }
        public Insert CreateInsert(IDatabaseContainer databaseContainer, string databaseName, string tableName)
        {
            Insert insert = new Insert(databaseContainer);
            insert.targetDatabase = databaseName;
            insert.targetTableName = tableName;
            return insert;
        }

        [TestMethod]
        public void Update_BadArguments_ConcretelyTableDoesntExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("TestUpdate1");
            databaseContainer.AddDatabase(database);
            string doenstExistTableNames = VariousFunctions.GenerateRandomString(6);
            while (database.ExistTable(doenstExistTableNames))
            {
                doenstExistTableNames = VariousFunctions.GenerateRandomString(6);
            }
            Assert.IsFalse(database.ExistTable(doenstExistTableNames));
            Update update = CreateUpdate(databaseContainer, database.databaseName, doenstExistTableNames);
            Assert.IsFalse(update.ValidateParameters());
            update.Execute();
        }
        [TestMethod]
        public void Update_BadArguments_TheDataTypesDontMatch_NoticeInValidateParameters()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("TestUpdate2");
            ITable table = new Table("table1");
            Column column1 = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2 = new Column("c2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table.AddColumn(column1);
            table.AddColumn(column2);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            string[] rowData = new string[] { "aaa", "aaa" };
            Assert.IsFalse(column1.dataType.IsAValidDataType(rowData[0]) && column2.dataType.IsAValidDataType(rowData[1]));
            int rowCount = table.GetRowCount();
            Update update = CreateUpdate(databaseContainer, database.databaseName, table.tableName);
            update.AddValue(column1.columnName,rowData[0]);
            update.AddValue(column2.columnName, rowData[1]);
            Assert.IsFalse(update.ValidateParameters());
            update.Execute();
            Assert.AreEqual(rowCount, table.GetRowCount());
        }
        [TestMethod]
        public void Update_GodArguments_TheQueryUpdateTheNewsRows()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string c1FirstRowData = "aaa";
            string c2FirstRowData = "1";
            string c1SecondRowData = "aaaa";
            string c2SecondRowData = "2";
            Database database = new Database("TestInsert3");
            ITable table = new Table("table1");
            Column column1 = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2 = new Column("c2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            Assert.IsTrue(column1.dataType.IsAValidDataType(c1FirstRowData));
            Assert.IsTrue(column2.dataType.IsAValidDataType(c2FirstRowData));
            Assert.IsTrue(column1.dataType.IsAValidDataType(c1SecondRowData));
            Assert.IsTrue(column2.dataType.IsAValidDataType(c2SecondRowData));
            table.AddColumn(column1);
            table.AddColumn(column2);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            int rowCount = table.GetRowCount();
            Select firstSelect = TestSelect.CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            firstSelect.ValidateParameters();
            firstSelect.Execute();
            Assert.AreEqual(0, firstSelect.GetAfectedRowCount());
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table.tableName);
            insert.AddValue(c1FirstRowData);
            insert.AddValue(c2FirstRowData);
            Assert.IsTrue(insert.ValidateParameters());
            insert.Execute();
            Select secondSelect = TestSelect.CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            secondSelect.ValidateParameters();
            secondSelect.Execute();
            Assert.IsTrue(firstSelect.GetAfectedRowCount() < secondSelect.GetAfectedRowCount());
            Assert.IsTrue(column1.GetCells(c1FirstRowData).Count > 0);
            Assert.IsTrue(column2.GetCells(c2FirstRowData).Count > 0);
            Assert.AreEqual(rowCount + 1, table.GetRowCount());
            Update update = CreateUpdate(databaseContainer, database.databaseName, table.tableName);
            update.AddValue(column1.columnName,c1FirstRowData);
            update.AddValue(column2.columnName,c2FirstRowData);
            Assert.IsTrue(update.ValidateParameters());
            update.Execute();
            Select thirdSelect = TestSelect.CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            thirdSelect.ValidateParameters();
            thirdSelect.Execute();
            Assert.IsTrue(firstSelect.GetAfectedRowCount() < thirdSelect.GetAfectedRowCount());
            Assert.IsTrue(column1.GetCells(c1FirstRowData).Count > 0);
            Assert.IsTrue(column2.GetCells(c2FirstRowData).Count > 0);
            Assert.AreEqual(rowCount + 1, table.GetRowCount());
        }
        [TestMethod]
        public void Update_BadArguments_ConcretelyDatabaseDoesntExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string doenstExistDatabaseNames = VariousFunctions.GenerateRandomString(6);
            while (databaseContainer.ExistDatabase(doenstExistDatabaseNames))
            {
                doenstExistDatabaseNames = VariousFunctions.GenerateRandomString(6);
            }
            Assert.IsFalse(databaseContainer.ExistDatabase(doenstExistDatabaseNames));
            Update update = CreateUpdate(databaseContainer, doenstExistDatabaseNames, "aa");
            update.AddValue("c1","zz");
            Assert.IsFalse(update.ValidateParameters());
            update.Execute();
        }
    }
}
