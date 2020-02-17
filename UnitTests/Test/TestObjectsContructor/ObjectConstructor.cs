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





    }
}
