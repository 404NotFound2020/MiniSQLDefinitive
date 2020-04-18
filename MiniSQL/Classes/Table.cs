using MiniSQL.Comparers;
using MiniSQL.Interfaces;
using MiniSQL.TableRestrictions;
using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Table : ITable
	{
		public override string tableName { get; set; }
		public override PrimaryKey primaryKey { get; }
		public override ForeignKey foreignKey { get; }
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

		public override void AddRow(Row row)
		{
			IEnumerator<Column> columnEnumerator = this.columnsOrdened.GetEnumerator();
			while (columnEnumerator.MoveNext())
			{
				columnEnumerator.Current.AddCell(row.GetCell(columnEnumerator.Current.columnName));
			}
			rows.Add(row);
			this.primaryKey.AddUsedKey(row);
		}

		public override Row CreateRowDefinition()
		{
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

		public override bool IsDropable()
		{
			return this.numberOfReferencesAtThisTable == 0;
		}

		public override bool DestroyRow(int rowNumber)
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
				this.primaryKey.RemoveUsedKey(row);
				this.rows.RemoveAt(rowNumber);
				b = true;
			}
			return b;
		}

		public override void AddColumn(Column column)
		{
			columns.Add(column.columnName, column);
			columnsOrdened.Add(column);
			column.table = this;
			this.AddCellBecauseOfColumnAddition(column);
		}

		private void AddCellBecauseOfColumnAddition(Column column) {
			IEnumerator<Row> rowEnumerator = this.rows.GetEnumerator();
			Cell cell;
			while (rowEnumerator.MoveNext())
			{
				cell = new Cell(column, column.dataType.GetDataTypeDefaultValue(), rowEnumerator.Current);
				rowEnumerator.Current.AddCell(cell);
				column.AddCell(cell);
			}
		}


		public override Column GetColumn(string columnName)
		{
			return columns[columnName];
		}

		public override bool ExistColumn(string columnName)
		{
			return this.columns.ContainsKey(columnName);
		}

		public override IEnumerator<Column> GetColumnEnumerator()
		{
			return this.columnsOrdened.GetEnumerator();
		}

		public override IEnumerator<Row> GetRowEnumerator()
		{
			return this.rows.GetEnumerator();
		}

		public override int GetRowCount()
		{
			return this.rows.Count;
		}

		public override int GetColumnCount()
		{
			return this.columnsOrdened.Count;
		}

		protected override List<Column> GetColumnList()
		{
			return this.columnsOrdened;
		}

	}
}
