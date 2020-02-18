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

		}

		public void AddCell(Cell cell)
		{

		}


		public Cell GetCell(string columnName)
		{
			return null;
		}

		public void SetCellValue(string columnKey, string value) 
		{ 
			
		}

		public ReadOnlyDictionary<string, Cell> ReadCells() 
		{
			return new ReadOnlyDictionary<string, Cell>(this.cells);
		}
		


	}
}
