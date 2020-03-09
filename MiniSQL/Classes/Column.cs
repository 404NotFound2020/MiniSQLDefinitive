using MiniSQL.Comparers;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MiniSQL.Classes
{
	public class Column
	{
		public string columnName;
		private Dictionary<string, List<Cell>> cells;
		public DataType dataType;

		public Column(string columnName, DataType dataType)
		{
			this.columnName = columnName;
			this.dataType = dataType;
			cells = new Dictionary<string, List<Cell>>();

		}

		public void AddCell(Cell cell)
		{
			if (!cells.ContainsKey(cell.data)) cells.Add(cell.data, new List<Cell>());
			cells[cell.data].Add(cell);
		}

		public bool DestroyCell(Cell cell) 
		{
			bool b = false;
			if (this.ExistCells(cell.data))
			{
				List<Cell> cellList = this.cells[cell.data];
				IEqualityComparer<Row> rowComparer = Row.GetRowComparer();
				for (int i = 0; i < cellList.Count && b == false; i++)
				{
					b = rowComparer.Equals(cell.row, cellList[i].row);
					if (b)
					{
						cellList.RemoveAt(i);
						if (cellList.Count == 0)
						{
							this.cells.Remove(cell.data);
						}
					}
				}
			}
			return b;
		}

		public bool ExistCells(string cellData) 
		{
			return cells.ContainsKey(cellData);		
		}

		public List<Cell> GetCells(string data)
		{
			return cells[data];
		}

		public static IEqualityComparer<Column> GetColumnComparer()
		{
			return new ColumnComparer();
		}











		private class ColumnComparer : IEqualityComparer<Column>
		{
			public bool Equals(Column x, Column y)
			{
				if (!x.columnName.Equals(y.columnName))
					return false;
				if (!x.dataType.Equals(y.dataType))
					return false;
				return new DictionaryComparer<string, List<Cell>>(new ListComparer<Cell>(Cell.GetCellComparer())).Equals(x.cells, y.cells);
			}

			public int GetHashCode(Column obj)
			{
				throw new NotImplementedException();
			}
		}







	}



}
