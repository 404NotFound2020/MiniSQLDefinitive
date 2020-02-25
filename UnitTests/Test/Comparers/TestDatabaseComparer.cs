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
            string[] databaseAtributes = { "aaaa", "user1", "password1" };
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
            Database database2 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
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
            string[] databaseAtributes = { "aaaa", "user1", "password1" };
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
            Database database2 = new Database(databaseAtributes[0] + "b", databaseAtributes[1], databaseAtributes[2]);
            database1.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database1.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database1.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            database2.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database2.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database2.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            Assert.IsFalse(databaseComparer.Equals(database1, database2));
        }
        /**
        [TestMethod]
        public void Equals_TwoNoEqualDatabaseDiferencesInUser_ReturnFalse()
        {
            IEqualityComparer<Database> databaseComparer = CreateDatabaseComparer();
            string[] databaseAtributes = { "aaaa", "user1", "password1" };
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
            Database database2 = new Database(databaseAtributes[0], databaseAtributes[1] + "b", databaseAtributes[2]);
            database1.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database1.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database1.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            database2.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database2.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database2.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            Assert.IsFalse(databaseComparer.Equals(database1, database2));
        }
        **/
        /**
        [TestMethod]
        public void Equals_TwoNoEqualDatabaseDiferencesInPassword_ReturnFalse()
        {
            IEqualityComparer<Database> databaseComparer = CreateDatabaseComparer();
            string[] databaseAtributes = { "aaaa", "user1", "password1" };
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
            Database database2 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2] + "c");
            database1.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database1.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database1.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            database2.AddTable(TestTableComparer.CreateTableModel1(tableNames[0]));
            database2.AddTable(TestTableComparer.CreateTableModel2(tableNames[1]));
            database2.AddTable(TestTableComparer.CreateTableModel3(tableNames[2]));
            Assert.IsFalse(databaseComparer.Equals(database1, database2));
        }
        **/
        [TestMethod]
        public void Equals_TwoNoEqualDatabaseDiferencesInTablesContent_ReturnFalse()
        {
            IEqualityComparer<Database> databaseComparer = CreateDatabaseComparer();
            string[] databaseAtributes = { "aaaa", "user1", "password1" };
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
            Database database2 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
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
            string[] databaseAtributes = { "aaaa", "user1", "password1" };
            string[] tableNames = { "table1", "table2", "table3" };
            Database database1 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
            Database database2 = new Database(databaseAtributes[0], databaseAtributes[1], databaseAtributes[2]);
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
