using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
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
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            databaseContainer.AddDatabase(database);
            Select select = CreateSelect(databaseContainer);
            select.targetDatabase = database.databaseName;
            select.targetTableName = table.tableName;
            select.selectedAllColumns = true;
            select.whereClause = new Where();
            select.whereClause.AddCritery(new Tuple<string, string>("Column3", "180.5"), Operator.higher);
            select.ValidateParameters();
            select.Execute();
            Console.WriteLine(select.GetResult());

        }

        [TestMethod]
        public void Select_GoodArguments_ShouldntFindResults()
        {

        }

        [TestMethod]
        public void Select_BadArguments_TableDoesntExist_NotifiedWithInResult()
        {

        }

        [TestMethod]
        public void Select_BadArguments_SelectedColumnsDontExist_NotifiedWithInResult()
        {

        }

        [TestMethod]
        public void Select_BadArguments_WhereClauseColumnsDontExist_NotifiedInResult()
        {

        }

        public static Select CreateSelect(IDatabaseContainer databaseContainer)
        {
            return new Select(databaseContainer);
        }


    }
}
