using MiniSQL.Comparers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MiniSQL.Classes
{
	public class Database
	{
		public string databaseName;
		private Dictionary<string, Table> tables;

		public Database(string databaseName)
		{
			this.databaseName = databaseName;
			this.tables = new Dictionary<string, Table>();
		}

		public bool ExistTable(string tableName)
		{
			return this.tables.ContainsKey(tableName);
		}

		public void DropTable(string tableName)
		{
			this.tables.Remove(tableName);
		}

		public void AddTable(Table table)
		{
			this.tables.Add(table.tableName,table);
		}

		public Table GetTable(string tableName)
		{
			return this.tables[tableName];
		}

		public IEnumerator<Table> GetTableEnumerator()
		{
			return this.tables.Values.GetEnumerator();
		}

		public static IEqualityComparer<Database> GetDatabaseComparer()
		{
			return new DatabaseComparer();
		}

		








		private class DatabaseComparer : IEqualityComparer<Database>
		{
			public bool Equals(Database x, Database y)
			{
				if (!x.databaseName.Equals(y.databaseName))
					return false;
				return new DictionaryComparer<string, Table>(Table.GetTableComparer()).Equals(x.tables, y.tables);
			}

			public int GetHashCode(Database obj)
			{
				throw new NotImplementedException();
			}
		}



	}
}
