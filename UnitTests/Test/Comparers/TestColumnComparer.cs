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
    public class TestColumnComparer
    {
        [TestMethod]
        public void Equals_TwoEqualColumn_ReturnTrue()
        {
            ColumnComparer columnComparer = CreateColumnComparer();
            List<string> cellData = ObjectConstructor.CreateStringTypeRandomCellData(5, 5);
            string columnName = "columnX";
            Column column1 = ObjectConstructor.CreateColumn(cellData, TypesKeyConstants.StringTypeKey, columnName);
            Column column2 = ObjectConstructor.CreateColumn(cellData, TypesKeyConstants.StringTypeKey, columnName);
            Assert.IsTrue(columnComparer.Equals(column1, column2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualColumnDiferencesInName_ReturnFalse()
        {
            ColumnComparer columnComparer = CreateColumnComparer();
            List<string> cellData = ObjectConstructor.CreateStringTypeRandomCellData(5, 5);
            string columnName1 = "columnX";
            string columnName2 = "columnY";
            Column column1 = ObjectConstructor.CreateColumn(cellData, TypesKeyConstants.StringTypeKey, columnName1);
            Column column2 = ObjectConstructor.CreateColumn(cellData, TypesKeyConstants.StringTypeKey, columnName2);
            Assert.IsFalse(columnName1.Equals(columnName2));
            Assert.IsFalse(columnComparer.Equals(column1, column2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualColumnDiferencesInContent_ReturnFalse() 
        {
            ColumnComparer columnComparer = CreateColumnComparer();
            List<string> cellData1 = new List<string>();
            cellData1.Add("aa2");
            List<string> cellData2 = new List<string>();
            cellData2.Add("bb");
            string columnName = "columnX";
            Column column1 = ObjectConstructor.CreateColumn(cellData1, TypesKeyConstants.StringTypeKey, columnName);
            Column column2 = ObjectConstructor.CreateColumn(cellData2, TypesKeyConstants.StringTypeKey, columnName);
            Assert.IsTrue(cellData1.Count == 1 && cellData2.Count == 1);
            Assert.IsFalse(cellData1[0].Equals(cellData2[0]));
            Assert.IsFalse(columnComparer.Equals(column1, column2));
        }


        public static ColumnComparer CreateColumnComparer()
        {
            return new ColumnComparer();
        }

    }

}
