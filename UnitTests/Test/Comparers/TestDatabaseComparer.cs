using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;

namespace UnitTests.Test.Comparers
{
    [TestClass]
    public class TestDatabaseComparer
    {
        [TestMethod]
        public void Equals_TwoEqualDatabase_ReturnTrue()
        {
            IEqualityComparer<Database> databaseComparer = CreateDatabaseComparer();
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database("aaaa");
            Database database2 = new Database("aaaa");
            database1.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database1.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database1.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            database2.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database2.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database2.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            Assert.IsTrue(databaseComparer.Equals(database1, database2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualDatabaseDiferencesInName_ReturnFalse()
        {
            IEqualityComparer<Database> databaseComparer = CreateDatabaseComparer();
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database("aaaa");
            Database database2 = new Database("aaaa" + "b");
            database1.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database1.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database1.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            database2.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database2.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database2.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            Assert.IsFalse(databaseComparer.Equals(database1, database2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualDatabaseDiferencesInTablesContent_ReturnFalse()
        {
            IEqualityComparer<Database> databaseComparer = CreateDatabaseComparer();
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database("aaaa");
            Database database2 = new Database("aaaa");
            database1.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database1.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database1.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            database2.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database2.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database2.AddTable(TestTableComparer.CreateTableModel4(tableNames[2]));
            Assert.IsFalse(databaseComparer.Equals(database1, database2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualDatabaseDiferencesInTableNames_ReturnFalse()
        {            
            IEqualityComparer<Database> databaseComparer = CreateDatabaseComparer();
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database("aaaa");
            Database database2 = new Database("aaaa");
            database1.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database1.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database1.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            database2.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database2.AddTable(TestTableComparer.CreateTableModel2(tableNames[1] + "ff"));
            database2.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            Assert.IsFalse(databaseComparer.Equals(database1, database2));
        }



        public static IEqualityComparer<Database> CreateDatabaseComparer()
        {
            return Database.GetDatabaseComparer();
        }

    }
}
