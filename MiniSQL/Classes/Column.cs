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
			if (cells.ContainsKey(cell.data))
			{
				cells[cell.data].Add(cell);
			}
			else
			{
				List<Cell> list = new List<Cell>();
				list.Add(cell);
				cells.Add(cell.data, list);
			}
		}

		public bool ExistCells(string cellData) 
		{
			return cells.ContainsKey(cellData);		
		}

		public List<Cell> GetCells(string data)
		{
			return cells[data];
		}

		public ReadOnlyDictionary<string, List<Cell>> ReadCells()
		{
			return new ReadOnlyDictionary<string, List<Cell>>(this.cells);
		}

	}
}
