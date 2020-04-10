using MiniSQL.Comparers;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MiniSQL.Classes
{
	public class Database : IDatabase
	{
		private Dictionary<string, ITable> tables;

		public override string databaseName { get; set; }

		public Database(string databaseName)
		{
			this.databaseName = databaseName;
			this.tables = new Dictionary<string, ITable>();
		}

		public override bool ExistTable(string tableName)
		{
			return this.tables.ContainsKey(tableName);
		}

		public override void DropTable(string tableName)
		{
			this.tables.Remove(tableName);
		}

		public override void AddTable(ITable table)
		{
			this.tables.Add(table.tableName,table);
		}

		public override ITable GetTable(string tableName)
		{
			return this.tables[tableName];
		}

		public override IEnumerator<ITable> GetTableEnumerator()
		{
			return this.tables.Values.GetEnumerator();
		}
		
		protected override Dictionary<string, ITable> GetTables()
		{
			return this.tables;
		}

		



	}
}
