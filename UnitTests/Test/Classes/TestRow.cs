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
   
    [TestMethod]
        public void ExistCells_Exist_ReturnTrue()
        {
            Cell cell = createCell();
            Row row = new Row();
            row.AddCell(cell);
            Assert.IsTrue(row.ExistCell(cell));
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