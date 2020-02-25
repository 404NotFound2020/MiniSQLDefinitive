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
		public string user;
		public string password;

		public Database(string databaseName, string user, string password)
		{
			this.databaseName = databaseName;
			this.user = "aa";
			this.password = "aa";
			this.tables = new Dictionary<string, Table>();
		}

		public bool ExistTable(string tableName)
		{
			return this.tables.ContainsKey(tableName);
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
				if (!(x.user.Equals(y.user) && x.password.Equals(y.password)))
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
