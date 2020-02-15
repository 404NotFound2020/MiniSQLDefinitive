using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Column
	{
		public string columnName;
		private Dictionary<string, List<Cell>> cells;
		public DataType dataType;

		public Column(string columnName, DataType tdataType)
		{



		}


		public void AddCell(Cell cell)
		{

		}

		public bool ExistCells(string cellData) 
		{
			return false;
		}

		public List<Cell> GetCells(string data)
		{
			return null;
		}




	}
}
