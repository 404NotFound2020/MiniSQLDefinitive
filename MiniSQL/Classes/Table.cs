using MiniSQL.Comparers;
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
			IEnumerator<Column> columnEnumerator = this.columnsOrdened.GetEnumerator();
			while (columnEnumerator.MoveNext()) 
			{
				columnEnumerator.Current.AddCell(row.GetCell(columnEnumerator.Current.columnName));
			}
			rows.Add(row);
		}

		public Row CreateRowDefinition() {			
			Row r = new Row();			
			foreach (Column c in columnsOrdened)
			{
				Cell cl = new Cell(c, null, r);
				r.AddCell(cl);
			}
			return r;			
		}

		public void AddColumn(Column column)
		{
			columns.Add(column.columnName, column);
			columnsOrdened.Add(column);
		}

		public Column GetColumn(string columnName)
		{
			return columns[columnName];
		}

		public bool ExistColumn(string columnName)
		{
			return this.columns.ContainsKey(columnName);	
		}

		public IEnumerator<Column> GetColumnEnumerator()
		{
			return this.columnsOrdened.GetEnumerator();
		}

		public IEnumerator<Row> GetRowEnumerator() 
		{
			return this.rows.GetEnumerator();
		}

		public static IEqualityComparer<Table> GetTableComparer()
		{
			return new TableComparer();
		}











		private class TableComparer : IEqualityComparer<Table>
		{
			public bool Equals(Table x, Table y)
			{
				if (!x.tableName.Equals(y.tableName))
					return false;
				return new ListComparer<Column>(Column.GetColumnComparer()).Equals(x.columnsOrdened, y.columnsOrdened);
			}

			public int GetHashCode(Table obj)
			{
				throw new NotImplementedException();
			}
		}


	}
}
