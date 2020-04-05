using MiniSQL.Comparers;
using MiniSQL.TableRestrictions;
using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Table
	{
		public string tableName;
		public PrimaryKey primaryKey;
		public ForeignKey foreignKey;
		private Dictionary<string, Column> columns;
		private List<Row> rows;
		private List<Column> columnsOrdened;
		private int numberOfReferencesAtThisTable;

		public Table(string tableName)
		{
			this.tableName = tableName;
			columns = new Dictionary<string, Column>();
			rows = new List<Row>();
			columnsOrdened = new List<Column>();
			this.primaryKey = new PrimaryKey(this);
			this.numberOfReferencesAtThisTable = 0;
			this.foreignKey = new ForeignKey(this);
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
				Cell cl = new Cell(c, c.dataType.GetDataTypeDefaultValue(), r);
				r.AddCell(cl);
			}
			return r;			
		}

		public void IncrementNumberOfReferencesAtThisTable()
		{
			this.numberOfReferencesAtThisTable = this.numberOfReferencesAtThisTable + 1;
		}

		public void DecrementNumberOfReferencesAtThisTable()
		{
			this.numberOfReferencesAtThisTable = this.numberOfReferencesAtThisTable - 1;
		}

		public bool IsDropable() 
		{
			return this.numberOfReferencesAtThisTable == 0;
		}

		public bool DestroyRow(int rowNumber) 
		{
			bool b = false;
			if (rowNumber < this.GetRowCount())
			{
				Row row = this.rows[rowNumber];
				IEnumerator<Cell> cellEnumerator = row.GetCellEnumerator();
				while (cellEnumerator.MoveNext())
				{
					cellEnumerator.Current.column.DestroyCell(cellEnumerator.Current);
				}
				this.rows.RemoveAt(rowNumber);
				b = true;
			}
			return b;
		}

		public void AddColumn(Column column)
		{
			columns.Add(column.columnName, column);
			columnsOrdened.Add(column);
			column.table = this;
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

		public int GetRowCount() 
		{
			return this.rows.Count;
		}

		public int GetColumnCount() 
		{
			return this.columnsOrdened.Count;
		}

		private class TableComparer : IEqualityComparer<Table>
		{
			public bool Equals(Table x, Table y)
			{
				if (!x.tableName.Equals(y.tableName))
					return false;
				return new ListComparer<Column>(Column.GetColumnComparer()).Equals(x.columnsOrdened, y.columnsOrdened) && PrimaryKey.GetPrimaryKeyComparer().Equals(x.primaryKey, y.primaryKey);
			}

			public int GetHashCode(Table obj)
			{
				throw new NotImplementedException();
			}
		}


	}
}
