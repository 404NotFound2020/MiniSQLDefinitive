﻿using System;
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

		public ReadOnlyDictionary<string, Cell> ReadCells() 
		{
			return new ReadOnlyDictionary<string, Cell>(this.cells);
		}
		public bool ExistCells(string cellData)
		{
			return cells.ContainsKey(cellData);
		}


	}
}
