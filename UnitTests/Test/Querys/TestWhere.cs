using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestWhere
    {
        [TestMethod]
        public void Where_EvaluateARow_RowFulfillTheCritery_CriteryIsOnStringTypeColumn_ReturnTrue()
        {
            Where where = CreateWhere();
            ITable table = new Table("testTable");
            Column column = new Column("C1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            Cell cell = row.GetCell(column.columnName);
            cell.data = "aaaa";
            where.AddCritery(new Tuple<string, string>(column.columnName, cell.data), Operator.equal);
            Assert.IsTrue(where.IsSelected(row));
        }

        [TestMethod]
        public void Where_EvaluateARow_RowFulfillTheCritery_CriteryIsOnIntTypeColumn_ReturnTrue()
        {
            Where where = CreateWhere();
            ITable table = new Table("testTable");
            Column column = new Column("C1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            Cell cell = row.GetCell(column.columnName);
            cell.data = "1";
            where.AddCritery(new Tuple<string, string>(column.columnName, cell.data), Operator.equal);
            Assert.IsTrue(where.IsSelected(row));
        }

        [TestMethod]
        public void Where_EvaluateARow_RowFulfillTheCritery_CriteryIsOnDoubleTypeColumn_ReturnTrue()
        {
            Where where = CreateWhere();
            ITable table = new Table("testTable");
            Column column = new Column("C1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            Cell cell = row.GetCell(column.columnName);
            cell.data = "1.3";
            where.AddCritery(new Tuple<string, string>(column.columnName, cell.data), Operator.equal);
            Assert.IsTrue(where.IsSelected(row));
        }

        [TestMethod]
        public void Where_EvaluateARowNotFulfillTheCritery_CriteryIsOnStringTypeColumn_ReturnFalse()
        {
            Where where = CreateWhere();
            ITable table = new Table("testTable");
            Column column = new Column("C1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            Cell cell = row.GetCell(column.columnName);
            cell.data = "aaa";
            where.AddCritery(new Tuple<string, string>(column.columnName, cell.data + "b"), Operator.equal);
            Assert.IsFalse(where.IsSelected(row));
        }

        [TestMethod]
        public void Where_EvaluateARowNotFulfillTheCritery_CriteryIsOnIntTypeColumn_ReturnFalse() 
        {
            Where where = CreateWhere();
            ITable table = new Table("testTable");
            Column column = new Column("C1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            Cell cell = row.GetCell(column.columnName);
            cell.data = "1";
            Assert.IsTrue(int.TryParse(cell.data, out int value));
            where.AddCritery(new Tuple<string, string>(column.columnName, (value + 1) + ""), Operator.equal);
            Assert.IsFalse(where.IsSelected(row));
        }

        [TestMethod]
        public void Where_EvaluateARowNotFulfillTheCritery_CriteryIsOnDoubleTypeColumn_ReturnFalse()
        {
            Where where = CreateWhere();
            ITable table = new Table("testTable");
            Column column = new Column("C1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey));
            table.AddColumn(column);
            Row row = table.CreateRowDefinition();
            Cell cell = row.GetCell(column.columnName);
            cell.data = "1.3";
            Assert.IsTrue(double.TryParse(cell.data, out double value));
            where.AddCritery(new Tuple<string, string>(column.columnName, value + 1 + ""), Operator.equal);
            Assert.IsFalse(where.IsSelected(row));
        }




        public static Where CreateWhere() 
        {
            return new Where();
        }

    }
}
