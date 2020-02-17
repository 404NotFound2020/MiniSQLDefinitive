using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Clases;
using MiniSQL.Classes;
using MiniSQL.Constants;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test
{
    [TestClass]
    public class TestRow
    {
        public static Row CreateStringCellData(Row r)
        {
            List<string> dataToWork = TestColumn.CreateStringCellData();
            Table t = new Table("Test");
            t.AddColumn(new Column("P1",tdataType));
            t.AddRow(r);
            r.AddCell(new Cell(t.GetColumn("P1"), "D1", r));
            r.AddCell(new Cell(t.GetColumn("P1"), "D2", r));
            r.AddCell(new Cell(t.GetColumn("P1"), "D3", r));
            r.AddCell(new Cell(t.GetColumn("P1"), "D4", r));
            r.AddCell(new Cell(t.GetColumn("P1"), "D5", r));
            r.AddCell(new Cell(t.GetColumn("P1"), "D6", r));
            r.AddCell(new Cell(t.GetColumn("P1"), "D7", r));
            return r;
        }

    
    [TestMethod]
        public void ExistCells_Exist_ReturnTrue_StrType()
        {
            Row row = new Row();
            row = TestRow.CreateStringCellData(row);
            Assert.IsNotNull(row.GetCell("P1"));

            Assert.IsTrue(dataToWork.Count > 0);
            Assert.IsTrue(column.ExistCells(dataToWork[0]));
        }
        [TestMethod]
        public void AddCell(Cell cell)
        {
        }
        [TestMethod]
        public void GetCell(string columnName)
        {
        }

    }
    ___________


            [TestMethod]
            public void ExistCells_Exist_ReturnTrue_StrType()
            {
                List<string> dataToWork = TestColumn.CreateStringCellData();
                Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaa");
                Assert.IsTrue(dataToWork.Count > 0);
                Assert.IsTrue(column.ExistCells(dataToWork[0]));
            }

            [TestMethod]
            public void ExistCells_NoExist_ReturnFalse_StrType()
            {
                List<string> dataToWork = TestColumn.CreateStringCellData();
                Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaaa");
                string randomStr = "uwu";
                while (dataToWork.Contains(randomStr))
                {
                    randomStr = VariousFunctions.GenerateRandomString(8);
                }
                Assert.IsFalse(dataToWork.Contains(randomStr));
                Assert.IsTrue(dataToWork.Count > 0);
                Assert.IsFalse(column.ExistCells(randomStr));
            }

            [TestMethod]
            public void AddCell_NoExistsCellsWithSameData_StrType()
            {
                List<string> dataToWork = TestColumn.CreateStringCellData();
                Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaaa");
                string randomStr = "uwu";
                while (dataToWork.Contains(randomStr))
                {
                    randomStr = VariousFunctions.GenerateRandomString(8);
                }
                Assert.IsFalse(dataToWork.Contains(randomStr));
                Assert.IsFalse(column.ExistCells(randomStr));
                column.AddCell(ObjectConstructor.CreateCell(column, randomStr, null));
                Assert.IsTrue(column.ExistCells(randomStr));
            }

            [TestMethod]
            public void AddCell_ExistCellsWithSameData_StrType()
            {
                List<string> dataToWork = TestColumn.CreateStringCellData();
                Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaaa");
                int index = 0;
                Assert.IsTrue(dataToWork.Count > 0);
                Assert.IsTrue(column.ExistCells(dataToWork[index]));
                int numberOfCellWithSameDataBeforeAddition = column.GetCells(dataToWork[index]).Count;
                column.AddCell(ObjectConstructor.CreateCell(column, dataToWork[index], null));
                Assert.AreEqual(numberOfCellWithSameDataBeforeAddition + 1, column.GetCells(dataToWork[index]).Count);
            }

            public static List<string> CreateStringCellData()
            {
                List<string> cellData = new List<string>();
                cellData.Add("P1");
                cellData.Add("P2");
                cellData.Add("P3");
                cellData.Add("P4");
                cellData.Add("P5");
                cellData.Add("P6");
                cellData.Add("P7");
                return cellData;
            }






}

