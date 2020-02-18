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
    public class TestRowComparer
    {
        [TestMethod]
        public void Equals_TwoEqualRow_ReturnTrue()
        {
            RowComparer rowComparer = CreateRowComparer();
            List<string> sameCellData = ObjectConstructor.CreateStringTypeRandomCellData(5, 2);
            List<Column> columnList1 = new List<Column>();
            List<Column> columnList2 = new List<Column>();
            columnList1.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList1.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));

            columnList2.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList2.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));

            Row row1 = ObjectConstructor.CreateRow(sameCellData, columnList1);
            Row row2 = ObjectConstructor.CreateRow(sameCellData, columnList2);
            Assert.IsTrue(rowComparer.Equals(row1, row2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualRowSameColumnDiferentData_ReturnFalse()
        {
            RowComparer rowComparer = CreateRowComparer();
            List<string> rowData1 = new List<string>();
            List<string> rowData2 = new List<string>();
            rowData1.Add("aa");
            rowData2.Add("cc");
            rowData2.Add("bb");
            rowData2.Add("dd");
            List<Column> columnList1 = new List<Column>();
            List<Column> columnList2 = new List<Column>();
            columnList1.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList1.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));
            columnList2.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList2.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col2"));
            Row row1 = ObjectConstructor.CreateRow(rowData1, columnList1);
            Row row2 = ObjectConstructor.CreateRow(rowData2, columnList2);
            Assert.IsFalse(rowComparer.Equals(row1, row2));
        }

        [TestMethod]
        public void Equals_TwoNoEqualRowSameDataWithDiferentColumns_ReturnFalse()
        {
            RowComparer rowComparer = CreateRowComparer();
            List<string> sameCellData = ObjectConstructor.CreateStringTypeRandomCellData(5, 2);
            List<Column> columnList1 = new List<Column>();
            List<Column> columnList2 = new List<Column>();
            columnList1.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList1.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col8"));
            columnList2.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col1"));
            columnList2.Add(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "col76"));
            Row row1 = ObjectConstructor.CreateRow(sameCellData, columnList1);
            Row row2 = ObjectConstructor.CreateRow(sameCellData, columnList2);
            Assert.IsFalse(rowComparer.Equals(row1, row2));
        }

        public static RowComparer CreateRowComparer()
        {
            return new RowComparer();
        }
    }
}
