using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Table
	{
		public string tableName;
		private Dictionary<string, Column> columns;
		private List<Row> rows;
		public Column topColumn;

		public Table(string tableName)
		{

		}

		public void AddRow(Row row)
		{
			
		}

		public Row CreateRowDefinition() {
			return null;
		}

		public void AddColumn(Column column)
		{

		}

		public Column GetColumn(string columnName)
		{			
			return null;
		}

		public bool ExistColumn(string columnName)
		{
			return false;
		}


	}
}
