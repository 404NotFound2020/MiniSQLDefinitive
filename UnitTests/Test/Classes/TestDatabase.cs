using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;

namespace UnitTests.Test
{
    [TestClass]
    public class TestDatabase
    {
        [TestMethod]
        public void ExistTable_Exist_ReturnTrue()
        {
            Table table = createTable();
            Database db = createDatabase();
            db.AddTable(table);
            Assert.AreEqual(true,db.ExistTable(table.tableName));
        }

        [TestMethod]
        public void ExistTable_NoExist_ReturnFalse()
        {
            Table table = createTable();
            Database db = createDatabase();
            Assert.AreEqual(false, db.ExistTable(table.tableName));
        }

        [TestMethod]
        public void AddTable_NoExist_NoThrowException()
        {
            Table table = createTable();
            Database db = createDatabase();
            db.AddTable(table);
            if (db.ExistTable(table.tableName))
            {
                Assert.AreEqual(false,true);
            }
            else 
            {
                Assert.AreEqual(false, false);
            }
        }

        [TestMethod]
        public void AddTable_Exist_ThrowException()
        {
            Table table = createTable();
            Database db = createDatabase();
            db.AddTable(table);
            if (db.ExistTable(table.tableName))
            {
                Assert.AreEqual(true, true);
            }
            else
            {
                Assert.AreEqual(true, false);
            }
        }

        [TestMethod]
        public void GetTable_Exist_ReturnTheTable() 
        { 
            Table table = createTable();
            Database db = createDatabase();
            db.AddTable(table);
            Table tb2 = db.GetTable(table.tableName);
            if (table.Equals(tb2))
            {
                Assert.AreEqual(true,true);
            }
            else 
            {
                Assert.AreEqual(true, false);
            }
        }

        [TestMethod]
        public void GetTable_NoExist_ThrowException()
        {
            Table table = createTable();
            Database db = createDatabase();
            Table tb2 = db.GetTable(table.tableName);
            if (table.Equals(tb2))
            {
                Assert.AreEqual(false, true);
            }
            else
            {
                Assert.AreEqual(false, false);
            }
        }

        public static Table createTable()
        { 
            Table table = new Table("t1");
            return table;
        }

        public static Database createDatabase()
        { 
           Database db = new Database("db1","u1","p1");
           return db;
        }






    }
}
