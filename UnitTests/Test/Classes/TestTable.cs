using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using System;
using System.Collections.Generic;
using UnitTests.Test.TestObjectsContructor;

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
		public void CreateRowDefinition_ReturnWellFormedRowDefinition()
		{
			Table table = ObjectConstructor.CreateTable();
		}




	}
}
