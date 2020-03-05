using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
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

        public static Database CreateDatabaseFull(string databaseName) {
            Database database = new Database(databaseName);
            Table table = new Table("Table1");
            table.AddColumn(new Column("Column1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            table.AddColumn(new Column("Column2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey)));
            table.AddColumn(new Column("Column3", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey)));
            table.AddColumn(new Column("Column4", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey)));
            int j = 200;
            Row row;
            for(int i = 0; i < j; i++) 
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

    }
}
