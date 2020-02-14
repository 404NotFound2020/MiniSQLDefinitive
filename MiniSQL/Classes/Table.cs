using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Table
	{
		private string tableName;
		private Dictionary<string, Column> columns;
		private List<Row> rows;

		public Table(string tableName)
		{

		}

		public void AddRow(Row row)
		{

		}

		public void AddColumn(Column column)
		{

		}

		public Row GetRow(string columnName)
		{
			return null;
		}

		public bool ExistColumn(string columnName)
		{
			return false;
		}



	}
}
