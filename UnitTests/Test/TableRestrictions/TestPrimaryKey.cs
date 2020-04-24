using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.TableRestrictions
{
    [TestClass]
    public class UnitTest1
    {
        private static string[] testTableColumns = new string[] { "c1", "c2", "c3" };
        private static string tableName = "testTable";


        [TestMethod]
        public void OneColumnPk_InsertNewRowWithNoDuplicatedKey_EvalReturnTrue()
        {
            Table table = CreatePrimaryKeysTable();
            Column pkColumn = table.GetColumn(testTableColumns[0]);
            table.primaryKey.AddKey(pkColumn);
            string noDuplicatedKey = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn);
            Row row = table.CreateRowDefinition();
            row.GetCell(pkColumn.columnName).data = noDuplicatedKey;
            Assert.IsTrue(table.primaryKey.Evaluate(row));
        }

        [TestMethod]
        public void OneColumnPk_InsertNewRowWithDuplicatedKey_EvalReturnFalse()
        {
            //Construct phase
            Table table = CreatePrimaryKeysTable();
            Column pkColumn = table.GetColumn(testTableColumns[0]);
            table.primaryKey.AddKey(pkColumn);
            string noDuplicatedKey = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn);
            Row row = table.CreateRowDefinition();
            row.GetCell(pkColumn.columnName).data = noDuplicatedKey;
            table.AddRow(row);
            //Test
            row = table.CreateRowDefinition();
            row.GetCell(pkColumn.columnName).data = noDuplicatedKey;
            Assert.IsFalse(table.primaryKey.Evaluate(row));
        }

        private static Table CreatePrimaryKeysTable()
        {
            Table table = new Table(tableName);
            for (int i = 0; i < testTableColumns.Length; i++)
            {
                table.AddColumn(new Column(testTableColumns[i], DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            }
            return table;
        }

    }
}
