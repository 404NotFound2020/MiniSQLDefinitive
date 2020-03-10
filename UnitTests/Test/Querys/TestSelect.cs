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
    public class TestSelect
    {
        [TestMethod]
        public void Select_GoodArguments_ShouldFindResults()
        {
            Database database = ObjectConstructor.CreateDatabaseFull("test1");
            Table table = database.GetTable("Table1");
            table.AddRow(table.CreateRowDefinition());
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            databaseContainer.AddDatabase(database);
            Select select = CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            select.whereClause.AddCritery(new Tuple<string, string>("Column3", table.GetColumn("Column3").dataType.GetDataTypeDefaultValue()), Operator.equal);
            Assert.IsTrue(select.ValidateParameters());
            select.Execute();
            Assert.IsTrue(select.GetNumberOfResults() > 0);
        }

        [TestMethod]
        public void Select_SelectAColumnExplicitelyGoodArguments_ShouldFindResults()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("aa");
            Table table = new Table("table1");
            string columnName = "c1";
            Column column = new Column(columnName, DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            row.GetCell(columnName).data = "aaaa";
            table.AddRow(row);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            Select select = CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            select.whereClause.AddCritery(new Tuple<string, string>(columnName, row.GetCell(columnName).data), Operator.equal);
            Assert.IsTrue(select.ValidateParameters());
            select.Execute();
            Assert.IsTrue(select.GetNumberOfResults() > 0);
            Console.WriteLine(select.GetResult());
        }

        [TestMethod]
        public void Select_GoodArguments_TableEmpty_ShouldntFindResults()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();            
            Database database = new Database("aa");
            databaseContainer.AddDatabase(database); //Notice the references. (this database object references, no the 'referencias')
            Table table = new Table("table1");
            Column column = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            database.AddTable(table);
            Select select = CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            select.whereClause.AddCritery(new Tuple<string, string>(column.columnName, table.GetColumn(column.columnName).dataType.GetDataTypeDefaultValue()), Operator.equal);
            Assert.IsTrue(select.ValidateParameters());
            Assert.IsTrue(table.GetRowCount() == 0); //Maybe assert.equal
            select.Execute();
            Assert.IsTrue(select.GetNumberOfResults() == 0);
        }

        [TestMethod]
        public void Select_BadArguments_TableDoesntExist_NoticedInValidateParameters()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = ObjectConstructor.CreateDatabaseFull("test1");
            databaseContainer.AddDatabase(database);
            string noInDatabaseTableName = VariousFunctions.GenerateRandomString(10); //Do while instead of while is okkkkk            
            while (database.ExistTable(noInDatabaseTableName)) 
            { 
                noInDatabaseTableName = VariousFunctions.GenerateRandomString(10);
            }
            Select select = CreateSelect(databaseContainer, database.databaseName, noInDatabaseTableName, true);
            Assert.IsFalse(select.ValidateParameters());            
            select.Execute();
            Assert.AreEqual(0, select.GetNumberOfResults());
            Console.WriteLine(select.GetResult()); //It is only to see the message not to trace execution
        }

        [TestMethod]
        public void Select_BadArguments_SelectedColumnsDontExist_NoticedInValidateParameters()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("aa");            
            Table table = new Table("table1");
            Column column = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            database.AddTable(table);
            databaseContainer.AddDatabase(database); 
            string noInTableColumnName = VariousFunctions.GenerateRandomString(10);
            while (table.ExistColumn(noInTableColumnName)) 
            {
                noInTableColumnName = VariousFunctions.GenerateRandomString(10);
            }
            Select select = CreateSelect(databaseContainer, database.databaseName, table.tableName, false);
            select.AddSelectedColumnName(noInTableColumnName);
            Assert.IsFalse(select.ValidateParameters());
            select.Execute();
            Assert.AreEqual(0, select.GetNumberOfResults());
            Console.WriteLine(select.GetResult());
        }

        [TestMethod]
        public void Select_BadArguments_WhereClauseColumnsDontExist_NoticedInValidateParameters()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("aa");
            Table table = new Table("table1");
            Column column = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            string noInTableColumnName = VariousFunctions.GenerateRandomString(10);
            while (table.ExistColumn(noInTableColumnName))
            {
                noInTableColumnName = VariousFunctions.GenerateRandomString(10);
            }
            Select select = CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            select.whereClause.AddCritery(new Tuple<string, string>(noInTableColumnName, "a"), Operator.equal);
            Assert.IsFalse(select.ValidateParameters());
            select.Execute();
            Assert.AreEqual(0, select.GetNumberOfResults());
            Console.WriteLine(select.GetResult());
        }

        [TestMethod]
        public void Select_BadArguments_WhereClauseColumnsWithABadDataTypeData_NoticedInValidateParameters()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("aa");
            Table table = new Table("table1");
            string columnName = "c1";
            Column column = new Column(columnName, DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            row.GetCell(columnName).data = "1";
            table.AddRow(row);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            Select select = CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            select.whereClause.AddCritery(new Tuple<string, string>(columnName, "a"), Operator.equal);
            Assert.IsFalse(select.ValidateParameters());
            select.Execute();
            Assert.AreEqual(0, select.GetNumberOfResults());
            Console.WriteLine(select.GetResult());
        }

        public static Select CreateSelect(IDatabaseContainer databaseContainer, string databaseName, string tableName, bool selectedAll)
        {
            Select select = new Select(databaseContainer);
            select.targetDatabase = databaseName;
            select.targetTableName = tableName;
            select.selectedAllColumns = selectedAll;
            return select;
        }


    }
}
