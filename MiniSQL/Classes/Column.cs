using MiniSQL.ColumnBehaviours;
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
		public ColumnBehaviour columnBehaviour;

		public Column(string columnName, DataType dataType)
		{
			this.columnName = columnName;
			this.dataType = dataType;

		}


		public void AddCell(Cell cell)
		{
			cells[cell.data].Add(cell);
		}

		public bool ExistCells(string cellData) 
		{
			return false;
		}

		public List<Cell> GetCells(string data)
		{
			return cells[data];
		}

        public bool ExistColumn(string v)
        {
            throw new NotImplementedException();
        }


		public ReadOnlyDictionary<string, List<Cell>> ReadCells()
		{
			return new ReadOnlyDictionary<string, List<Cell>>(this.cells);
		}

	}
}
