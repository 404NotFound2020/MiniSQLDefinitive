using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Column
	{
		private string columnName;
		private Dictionary<string, List<Cell>> cells;
		private IDataType dataType;

		public Column()
		{



		}


		public void AddCell(Cell cell)
		{

		}

		public List<Cell> GetCells(string data)
		{
			return null;
		}


	}
}
