using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Clases;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
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
            Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "P1");
            t.AddColumn(column);
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
        public void ExistCells_Exist_ReturnTrue()
        {
            Row row = new Row();
            row = TestRow.CreateStringCellData(row);
            Assert.IsNotNull(row.GetCell("P1"));
        }
        [TestMethod]
        public void AddCell()
        {
            Cell cell = createCell();
            Row row = new Row();
            row.AddCell(cell);
            Assert.IsNotNull(row.GetCell(cell.column.columnName));
        }
        [TestMethod]
        public void GetCell()
        {
            String columnName = "C1"; 
            Row row = new Row();
            Cell c = new Cell(new Column(columnName, (DoubleType)DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey)), "data", null);
            row.AddCell(c);

            Assert.IsNotNull(row.GetCell(columnName));
        }

        public static Cell createCell()
        {   
            return new Cell(new Column("columnName", (DoubleType)DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey)),"data", null);
        }

    }
}