using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{

	public class Cell
	{

		public Column column;
		public string data;
		public Row row;

		public Cell(Column column, string data, Row row)
		{
			this.column = column;
			this.data = data;
			this.row = row;
		}

		public static IEqualityComparer<Cell> GetCellComparer() {
			return new CellComparer();
		}











		private class CellComparer : IEqualityComparer<Cell>
		{
			public bool Equals(Cell x, Cell y)
			{
				return x.data.Equals(y.data);
			}

			public int GetHashCode(Cell obj)
			{
				throw new NotImplementedException();
			}
		}

	}

	
}
