using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using MiniSQL.ServerFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    public class ObjectConstructor
    {

        public static Column CreateColumn(List<string> cellData, string dataType, string columnName)
        {
            Row row;
            Cell cell;
            Column column = new Column(columnName, DataTypesFactory.GetDataTypesFactory().GetDataType(dataType));
            for (int i = 0; i < cellData.Count; i++) {
                row = new Row();
                cell = ObjectConstructor.CreateCell(column, cellData[i], row);
                column.AddCell(cell);
                row.AddCell(cell);
            }
            return column;
        }

        public static Table CreateTable() 
        {
            Table  table = new Table("aaaa");
            table.AddColumn(CreateColumn(new List<string>(), TypesKeyConstants.StringTypeKey, "c1"));
            return table;
        }


        public static Cell CreateCell(Column column, string data, Row row) 
        {
            return new Cell(column, data, row);       
        }

        public static Database CreateDatabaseFull(string databaseName)
        {
            return CreateDatabaseFull(databaseName, 200);
        }

        public static Database CreateDatabaseFull(string databaseName, int iterations) {
            Database database = new Database(databaseName);
            Table table = new Table("Table1");
            table.AddColumn(new Column("Column1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            table.AddColumn(new Column("Column2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            table.AddColumn(new Column("Column3", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey)));
            table.AddColumn(new Column("Column4", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey)));
            Row row;
            for(int i = 0; i < iterations; i++) 
            {
                row = table.CreateRowDefinition();
                row.GetCell("Column1").data = VariousFunctions.GenerateRandomString(8);
                row.GetCell("Column2").data = VariousFunctions.GenerateRandomString(8);
                row.GetCell("Column3").data = i + ".6";
                row.GetCell("Column4").data = i + "";
                table.AddRow(row);
            }
            database.AddTable(table);
            return database;
        }
       
        public static List<string> CreateStringTypeRandomCellData(int lengthOfData, int numberOfData) 
        {
            List<string> data = new List<string>();
            for (int i = 0; i < numberOfData; i++) 
            {
                data.Add(VariousFunctions.GenerateRandomString(lengthOfData));
            }
            return data;        
        }

        public static Row CreateRow(List<string> cellData, List<Column> columns) 
        {
            Row row = new Row();
            for(int i = 0; i < cellData.Count; i++) 
            {
                row.AddCell(new Cell(columns[i], cellData[i], null));
            }
            return row;
        }

        public static Table CreateFullTable(string tableName, List<Column> columns, List<List<string>> cellData) {
            Table table = new Table(tableName);
            for (int i = 0; i < columns.Count; i++) 
            {
                table.AddColumn(columns[i]);
            }
            Row row;
            for(int i = 0; i < cellData.Count; i++) 
            {
                row = table.CreateRowDefinition();
                for(int j = 0; j < cellData[i].Count; j++) 
                {
                    row.SetCellValue(columns[j].columnName, cellData[i][j]);
                }
                table.AddRow(row);
            }
            return table;
        }

        public static Table CreateTableWithAColumnOfEachDataType()
        {
            List<string> colNames = new List<string>(){ "c1", "c2", "c3" };
            Table table = new Table("aaaa");
            table.AddColumn(new Column(colNames[0], DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            table.AddColumn(new Column(colNames[1], DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey)));
            table.AddColumn(new Column(colNames[2], DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey)));
            return table;
        }

        public static IDatabaseContainer CreateDatabaseContainer() 
        {
            return new DatabaseContainer();
        }

        public static Request GetSelectRequest()
        {
            return new Request("<transaction><fullQuery><![CDATA[SELECT * FROM test.aaaa;]]></fullQuery><query><![CDATA[SELECT]]></query><selectedColumn><![CDATA[*]]></selectedColumn><database><![CDATA[test]]></database><table><![CDATA[aaaa]]></table></transaction>");
        }

        public static Request GetInsertRequest()
        {
            return new Request("<transaction><fullQuery><![CDATA[INSERT INTO aaa.bbb VALUES(a);]]></fullQuery><query><![CDATA[INSERT]]></query><database><![CDATA[aaa]]></database><table><![CDATA[bbb]]></table><value><![CDATA[a]]></value></transaction>");
        }

        public static Request GetUpdateRequest()
        {
            return new Request("<transaction><fullQuery><![CDATA[UPDATE aaaa.aaa SET a=1 WHERE a=12;]]></fullQuery><query><![CDATA[UPDATE]]></query><database><![CDATA[aaaa]]></database><table><![CDATA[aaa]]></table><updatedColumn><![CDATA[a]]></updatedColumn><toValue><![CDATA[1]]></toValue><toEvalColumn><![CDATA[a]]></toEvalColumn><operator><![CDATA[=]]></operator><evalValue><![CDATA[12]]></evalValue></transaction>");
        }

        public static Request GetDeleteRequest()
        {
            return new Request("<transaction><fullQuery><![CDATA[DELETE FROM aaaa.nbbb WHERE a=1;]]></fullQuery><query><![CDATA[DELETE]]></query><database><![CDATA[aaaa]]></database><table><![CDATA[nbbb]]></table><toEvalColumn><![CDATA[a]]></toEvalColumn><operator><![CDATA[=]]></operator><evalValue><![CDATA[1]]></evalValue></transaction>");
        }

        public static Request GetCreateTableRequest()
        {
            return new Request("<transaction><fullQuery><![CDATA[CREATE TABLE aaa.aaa (a INT);]]></fullQuery><query><![CDATA[CREATE TABLE]]></query><database><![CDATA[aaa]]></database><table><![CDATA[aaa]]></table><column><![CDATA[a]]></column><columnType><![CDATA[INT]]></columnType></transaction>");
        }

        public static Request GetDropTableRequest()
        {
            return new Request("<transaction><fullQuery><![CDATA[DROP TABLE aaa.aaa;]]></fullQuery><query><![CDATA[DROP TABLE]]></query><database><![CDATA[aaa]]></database><table><![CDATA[aaa]]></table></transaction>");
        }

        public static string NewDatabaseName(IDatabaseContainer container)
        {
            string databaseName = VariousFunctions.GenerateRandomString(12);
            while (container.ExistDatabase(databaseName))
            {
                databaseName = VariousFunctions.GenerateRandomString(12);
            }
            return databaseName;
        }

        public static IUserThread GetFakeUserThread()
        {
            return new FakeUserThread();
        }

        public static DummySysteme CreateDummySysteme() {
            DummySysteme dummySysteme = new DummySysteme();
            dummySysteme.SetActiveModule(new DatabaseContainer());
            dummySysteme.SetActiveModule(new DummyPrivilegeModule());
            return dummySysteme;
        }

    }
}
