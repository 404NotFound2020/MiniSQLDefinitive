using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Table
	{
		public string tableName;
		private Dictionary<string, Column> columns;
		private List<Row> rows;
		private List<Column> columnsOrdened;

		public Table(string tableName)
		{
			this.tableName = tableName;
			columns = new Dictionary<string, Column>();
			rows = new List<Row>();
			columnsOrdened = new List<Column>();
		}

		public void AddRow(Row row)
		{
			rows.Add(row);
		}

		public Row CreateRowDefinition() {
			return null;
		}

		public void AddColumn(Column column)
		{
			columns.Add(column.columnName,column);
		}

		public Column GetColumn(string columnName)
		{
			Column c= null;
			c = columns[columnName];
			return c;
		}

		public bool ExistColumn(string columnName)
		{
			Column c = null;
			c = GetColumn(columnName);
			if (c == null)
			{
				return false;
			}
			else
			{
				return true;
			}		
		}

		public List<Column> GetColumnList() {
			List<Column> copyList = new List<Column>();
			this.columnsOrdened.ForEach((Column column) => copyList.Add(column));
			return copyList;
		}
	}
}
