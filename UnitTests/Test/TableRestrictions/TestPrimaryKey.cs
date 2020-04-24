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
    public class TestPrimaryKey
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


        [TestMethod]
        public void TwoColumnPk_InsertNewRowWithNoDuplicatedKey_EvalReturnTrue()
        {
            //Construct phase
            Table table = CreatePrimaryKeysTable();
            Column pkColumn1 = table.GetColumn(testTableColumns[0]);
            Column pkColumn2 = table.GetColumn(testTableColumns[1]);
            table.primaryKey.AddKey(pkColumn1);
            table.primaryKey.AddKey(pkColumn2);
            string noDuplicatedKey1 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn1);
            string noDuplicatedKey2 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn2);
            //Test
            Row row = table.CreateRowDefinition();
            row.GetCell(pkColumn1.columnName).data = noDuplicatedKey1;
            row.GetCell(pkColumn2.columnName).data = noDuplicatedKey2;
            Assert.IsTrue(table.primaryKey.Evaluate(row));
        }

        [TestMethod]
        public void TwoColumnPk_InsertNewRowWithDuplicatedKey_EvalReturnTrue()
        {
            //Construct phase
            Table table = CreatePrimaryKeysTable();
            Column pkColumn1 = table.GetColumn(testTableColumns[0]);
            Column pkColumn2 = table.GetColumn(testTableColumns[1]);
            table.primaryKey.AddKey(pkColumn1);
            table.primaryKey.AddKey(pkColumn2);
            string noDuplicatedKey1 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn1);
            string noDuplicatedKey2 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn2);
            Row row = table.CreateRowDefinition();
            row.GetCell(pkColumn1.columnName).data = noDuplicatedKey1;
            row.GetCell(pkColumn2.columnName).data = noDuplicatedKey2;
            table.AddRow(row);
            //Test
            row = table.CreateRowDefinition();
            row.GetCell(pkColumn1.columnName).data = noDuplicatedKey1;
            row.GetCell(pkColumn2.columnName).data = noDuplicatedKey2;
            Assert.IsFalse(table.primaryKey.Evaluate(row));
        }

        [TestMethod]
        public void XtremeCase_TwoColumnAsPK_NewRowWithNoPKDuplicated_ButIndividuallyPKColumnsValuesAreDuplicated_EvalReturnTrue()
        {
            //Construct phase
            Table table = CreatePrimaryKeysTable();
            Column pkColumn1 = table.GetColumn(testTableColumns[0]);
            Column pkColumn2 = table.GetColumn(testTableColumns[1]);
            table.primaryKey.AddKey(pkColumn1);
            table.primaryKey.AddKey(pkColumn2);
            string noDuplicatedKey1 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn1);
            string noDuplicatedKey2 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn2);
            Row row = table.CreateRowDefinition();
            row.GetCell(pkColumn1.columnName).data = noDuplicatedKey1;
            row.GetCell(pkColumn2.columnName).data = noDuplicatedKey2;
            table.AddRow(row);
            string noDuplicatedKey3 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn1);
            string noDuplicatedKey4 = ShittyUtilities.GenerateADoenstExistNewValue(pkColumn2);
            row = table.CreateRowDefinition();
            row.GetCell(pkColumn1.columnName).data = noDuplicatedKey3;
            row.GetCell(pkColumn2.columnName).data = noDuplicatedKey4;
            table.AddRow(row);
            //Test
            row = table.CreateRowDefinition();
            row.GetCell(pkColumn1.columnName).data = noDuplicatedKey1;
            row.GetCell(pkColumn2.columnName).data = noDuplicatedKey4;
            Assert.IsTrue(table.primaryKey.Evaluate(row));
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
