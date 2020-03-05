using MiniSQL.Comparers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MiniSQL.Classes
{
	public class Row
	{
		private Dictionary<string, Cell> cells;

		public Row()
		{
			cells = new Dictionary<string, Cell>();
		}

		public void AddCell(Cell cell)
		{
			cells.Add(cell.column.columnName, cell);
		}

		public Cell GetCell(string columnName)
		{
			return cells[columnName];
		}

		public void SetCellValue(string columnKey, string value) 
		{
			cells[columnKey].data = value;
		}

		public bool ExistCell(string columnName)
		{
			return cells.ContainsKey(columnName);
		}

		public IEnumerator<Cell> GetCellEnumerator()
		{
			return this.cells.Values.GetEnumerator();
		}

		public static IEqualityComparer<Row> GetRowComparer()
		{
			return new RowComparer();
		}













		private class RowComparer : IEqualityComparer<Row>
		{
			public bool Equals(Row x, Row y)
			{
				return new DictionaryComparer<string, Cell>(Cell.GetCellComparer()).Equals(x.cells, y.cells);
			}

			public int GetHashCode(Row obj)
			{
				throw new NotImplementedException();
			}
		}


	}
}
