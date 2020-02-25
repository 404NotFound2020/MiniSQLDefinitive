using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
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
            Column column = new Column(columnName, DataTypesFactory.GetDataTypesFactory().GetDataType(dataType));
            for (int i = 0; i < cellData.Count; i++) {
                column.AddCell(ObjectConstructor.CreateCell(column, cellData[i], null));
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

        public static Database CreateDatabaseFull() {
            Database database = new Database("Database1", "user", "user");
            Table table = new Table("Table1");
            table.AddColumn(new Column("Column1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            table.AddColumn(new Column("Column2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            table.AddColumn(new Column("Column3", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey)));
            table.AddColumn(new Column("Column4", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey)));
            Row row1 = table.CreateRowDefinition();
            row1.GetCell("Column1").data = "AAA";
            row1.GetCell("Column2").data = "AAA";
            row1.GetCell("Column3").data = "1.6";
            row1.GetCell("Column4").data = "1";
            table.AddRow(row1);
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

    }
}
