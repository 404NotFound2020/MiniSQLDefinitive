using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Comparers;
using MiniSQL.Constants;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Comparers
{
    [TestClass]
    public class TestTableComparer
    {
        [TestMethod]
        public void Equals_TwoEqualTable_ReturnTrue()
        {
            TableComparer tableComparer = CreateTableComparer();
            Table table1 = CreateTableModel1("SameTable");
            Table table2 = CreateTableModel1("SameTable");
            Assert.IsTrue(tableComparer.Equals(table1, table2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualTableDiferencesOnlyInName_ReturnFalse()
        {
            TableComparer tableComparer = CreateTableComparer();
            Table table1 = CreateTableModel1("SameTable");
            Table table2 = CreateTableModel1("NoTheSameTable");
            Assert.IsFalse(tableComparer.Equals(table1, table2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualTableDiferencesOnlyInContent_ReturnFalse()
        {
            TableComparer tableComparer = CreateTableComparer();
            Table table1 = CreateTableModel1("SameTable");
            Table table2 = CreateTableModel4("SameTable");
            Assert.IsFalse(tableComparer.Equals(table1, table2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualTableSameContentWithDiferentColumns_ReturnFalse()
        {
            TableComparer tableComparer = CreateTableComparer();
            Table table1 = CreateTableModel1("SameTable");
            Table table2 = CreateTableModel2("SameTable");
            Assert.IsFalse(tableComparer.Equals(table1, table2));
        }

        public static Table CreateTableModel1(string tableName) 
        {            
            List<Column> columnList = new List<Column>();
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col3"));
            List<List<string>> cellDatas = new List<List<string>>();
            cellDatas.Add(new List<string>());
            cellDatas.Add(new List<string>());
            cellDatas[0].Add("aaa");
            cellDatas[0].Add("aaafsdf");
            cellDatas[0].Add("a323f");
            cellDatas[1].Add("cccccc");
            cellDatas[1].Add("hhhhhh");
            cellDatas[1].Add("ffffff");
            return ObjectConstructor.CreateFullTable(tableName, columnList, cellDatas);       
        }

        public static Table CreateTableModel2(string tableName)
        {
            List<Column> columnList = new List<Column>();
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col8"));
            List<List<string>> cellDatas = new List<List<string>>();
            cellDatas.Add(new List<string>());
            cellDatas.Add(new List<string>());
            cellDatas[0].Add("aaa");
            cellDatas[0].Add("aaafsdf");
            cellDatas[0].Add("a323f");
            cellDatas[1].Add("cccccc");
            cellDatas[1].Add("hhhhhh");
            cellDatas[1].Add("ffffff");
            return ObjectConstructor.CreateFullTable(tableName, columnList, cellDatas);
        }

        public static Table CreateTableModel3(string tableName)
        {
            List<Column> columnList = new List<Column>();
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));
            List<List<string>> cellDatas = new List<List<string>>();
            cellDatas.Add(new List<string>());
            cellDatas.Add(new List<string>());
            cellDatas[0].Add("iu");
            cellDatas[0].Add("erf");
            cellDatas[1].Add("tt");
            cellDatas[1].Add("fjk");
            return ObjectConstructor.CreateFullTable(tableName, columnList, cellDatas);
        }

        public static Table CreateTableModel4(string tableName)
        {
            List<Column> columnList = new List<Column>();
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));
            columnList.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col3"));
            List<List<string>> cellDatas = new List<List<string>>();
            cellDatas.Add(new List<string>());
            cellDatas.Add(new List<string>());
            cellDatas[0].Add("aaa");
            cellDatas[0].Add("aaafsdf");
            cellDatas[0].Add("a323f");
            cellDatas[1].Add("cccccc");
            cellDatas[1].Add("hhhpij");
            cellDatas[1].Add("ffffff");
            return ObjectConstructor.CreateFullTable(tableName, columnList, cellDatas);
        }

        public static TableComparer CreateTableComparer()
        {
            return new TableComparer();
        }

    }
}
