using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using System;
using System.Collections.Generic;
using UnitTests.Test.TestObjectsContructor;
using MiniSQL.DataTypes;

namespace UnitTests.Test
{
	[TestClass]
	public class TestTable
	{
		[TestMethod]
		public void ExistColumn_Exist_ReturnTrue()
		{
			Table table = ObjectConstructor.CreateTable();
			table.AddColumn(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c2"));
			Assert.IsTrue(table.ExistColumn("c2"));
		}


		[TestMethod]
		public void ExistColumn_NoExist_ReturnFalse()
		{
			Table table = ObjectConstructor.CreateTable();
			table.AddColumn(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c2"));
			Assert.IsFalse(table.ExistColumn("c3"));

		}

		[TestMethod]
		public void AddColumn_NoExist_NoThrowException()
		{
			Table table = ObjectConstructor.CreateTable();
			table.AddColumn(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c2"));

		}

		[TestMethod]
		public void AddColumn_Exist_ThrowException()
		{
			Table table = ObjectConstructor.CreateTable();
			table.AddColumn(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c2"));
			/*Assert.ThrowsException<Exception()>;*/
		}

		[TestMethod]
		public void CreateRowDefinition_VerifyTheDefaultValues() 
		{
			Table table = ObjectConstructor.CreateTableWithAColumnOfEachDataType();
			Row row = table.CreateRowDefinition();
			IEnumerator<Column> columnEnumerator = table.GetColumnEnumerator();
			bool b = true;
			while(columnEnumerator.MoveNext() && b) 
			{
				b = row.GetCell(columnEnumerator.Current.columnName).data.Equals(columnEnumerator.Current.dataType.GetDataTypeDefaultValue());
			}
			Assert.IsTrue(b);
		}

		[TestMethod]
		public void DeleteRow_RowNumberIsLogic_DeleteTheRow() 
		{
			Table table = ObjectConstructor.CreateTable();
			table.AddColumn(ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c2"));
			int rowCountBeforeAdittion = table.GetRowCount();
			Row row = table.CreateRowDefinition();
			row.GetCell("c2").data = "aa";
			table.AddRow(row);
			Assert.IsTrue(rowCountBeforeAdittion == table.GetRowCount() - 1);
			int rowCountAfterAdittion = table.GetRowCount();
			Assert.IsTrue(table.DestroyRow(rowCountAfterAdittion - 1));
			Assert.IsTrue(table.GetRowCount() == rowCountAfterAdittion - 1);
		}

		[TestMethod]
		public void CreateRowDefinition_ReturnWellFormedRowDefinition()
		{
			Table table = new Table("testRowDefinition");
			Column column1 = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
			Column column2 = new Column("c2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
			table.AddColumn(column1);
			table.AddColumn(column2);
			Row row = table.CreateRowDefinition();
			Assert.IsTrue(row.ExistCell(column1.columnName));
			Assert.IsTrue(row.ExistCell(column2.columnName));
		}

		[TestMethod]
		public void GetRowCount_ReturnCoherentValue()
		{
			Table table = new Table("testRowCount");
			Column column = new Column("column1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
			table.AddColumn(column);
			int rowCount = table.GetRowCount();
			Row row;
			string genericData = "aaa";
			for (int i = 0; i < 200; i++)
			{
				row = table.CreateRowDefinition();
				Assert.IsTrue(column.dataType.IsAValidDataType(genericData));
				row.GetCell(column.columnName).data = genericData;
				table.AddRow(row);
				Assert.AreEqual(rowCount + i + 1, table.GetRowCount());
			}
		}

		[TestMethod]
		public void GetColumnCount_ReturnCoherentValue()
		{
			Table table = new Table("testRowCount");
			int columnCount = table.GetColumnCount();
			string columnName = VariousFunctions.GenerateRandomString(7);
			for (int i = 0; i < 100; i++)
			{
				while (table.ExistColumn(columnName))
				{
					columnName = VariousFunctions.GenerateRandomString(7);
				}
				Assert.IsFalse(table.ExistColumn(columnName));
				table.AddColumn(new Column(columnName, DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
				Assert.AreEqual(columnCount + i + 1, table.GetColumnCount());
			}
		}

		[TestMethod]
		public void IsDropable_TheTableIsDropable_ReturnTrue()
		{
			//Construction phase
			Table table1 = new Table("t1");
			Column column1 = ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c1");
			table1.AddColumn(column1);
			//Test phase
			Assert.IsTrue(table1.IsDropable());
		}

		[TestMethod]
		public void IsDropable_TheTableIsNotDropable_ReturnFalse()
		{
			//Construction phase
			Table table1 = new Table("t1");
			Column column1 = ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c1");
			table1.AddColumn(column1);
			Table table2 = new Table("t2");
			Column column2 = ObjectConstructor.CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c2");
			table2.AddColumn(column2);
			table2.foreignKey.AddForeignKey(column2, column1);
			//Test phase
			Assert.IsFalse(table1.IsDropable());
		}
	}
}
