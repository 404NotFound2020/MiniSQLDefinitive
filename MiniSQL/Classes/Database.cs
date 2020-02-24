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
			this.user = user;
			this.password = password;
			this.tables = new Dictionary<string, Table>();
		}

		public bool ExistTable(string tableName)
		{
			return tables.ContainsKey(tableName);
		}

		public void AddTable(Table table)
		{
			tables.Add(table.tableName,table);
		}

		public Table GetTable(string tableName)
		{
			return tables[tableName];
		}

		public ReadOnlyDictionary<string, Table> ReadTables()
		{
			return new ReadOnlyDictionary<string, Table>(this.tables);
		}




	}
}
